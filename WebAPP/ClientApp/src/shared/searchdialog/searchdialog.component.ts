import { Component, OnInit, EventEmitter, Injectable, Input, Output, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material';
import { Router } from '@angular/router';
import { PlaceSuggestion } from 'src/components/ui/address-autocomplete/address-autocomplete.component';
import { Activity } from 'src/dto/activity';
import { item } from 'src/dto/Items';
import { StayFilters } from 'src/dto/parseClass/stayFilters';
import { Stay } from 'src/dto/stay';

import { Thematic } from 'src/dto/thematic';

import { ActivityService } from 'src/services/activity-service';
import { StayService } from 'src/services/stay-service';
import { ThematicService } from 'src/services/thematic-service';
import { isNullOrUndefined } from 'util';
import { StructurePlay } from '../../dto/structdisplay';

import { StructureService } from '../../services/structure-service';


@Component({
  selector: 'searchdialog',
  templateUrl: './searchdialog.component.html',
  styleUrls: ['./searchdialog.component.css']
})

export class SearchdialogComponent implements OnInit {
  

    selected: Date | null;

    isLoading = false;


    @Input() imageUrl: string;
    @Input() title: string;
    @Input() heightSize: string;
    @Output() staysList = new EventEmitter<Array<Stay>>();

    moreFilters = false;
    
    typesOfMois : item[];
    typesOfvanc : item[];
    isSelected: any | Date;
    thematicList = new Array<Thematic>();
    yearList = ["2-8", "9-12", "13-16", "17-20", "21-30"];


    MoisDepart(){
    this.typesOfMois = [
        { value:1,label :"Janvier" },
        {value: 2,  label: "Fevrier" },
        { value:3, label: "Mars" },
        { value:4, label: "Avril" },
        { value:5, label: "Mai" },
        { value:6, label: "Juin"},
        { value:7, label:"Juillet" },
        { value:8, label: "Aout" },
        { value:9, label: "Septembre"},
        { value:10, label: "Octobre" },
        { value:11, label: "Novembre" },
        { value:12, label: "Decembre" },
    ];
   
}

Vacances(){
    this.typesOfvanc = [
        { value:1, label:"Vacances de la Toussaint-Oct/Nov" },
        { value:2, label: "Vacances de Noel-Dec/Janv" },
        { value:3, label: "Vacances hiver-Fev-Mars" },
        { value:4, label: "Vacances de printemps-Avr/Mai" },
        { value:5, label: "Vacances ete-Juin/Aout"},
       
    ]};



    
    
    activityList = new Array<Activity>();
    structureList = new Array<StructurePlay>();

    searchKeys: StayFilters;
    stayCity: string;

    constructor(private thematicSvc: ThematicService, private router: Router,
        private staySvc: StayService, private activitySvc: ActivityService, private structureSvc: StructureService, public dialog: MatDialog) { }

    async ngOnInit() {
        this.searchKeys = !isNullOrUndefined(JSON.parse(sessionStorage.getItem("LASTSEARCH"))) ? JSON.parse(sessionStorage.getItem("LASTSEARCH")) : new StayFilters();
        await this.loadThematicsList()
        await this.loadActivityList()
        await this.loadstructureList()
        this.MoisDepart();
        this.Vacances();
        
    }


    async loadThematicsList() {
        this.isLoading = true;

        this.thematicList = await this.thematicSvc.listThematic();
        this.isLoading = false;
    }

    async loadActivityList() {
        this.isLoading = true;
        this.activityList = await this.activitySvc.listActivity();
        this.isLoading = false;
    }

    async loadstructureList() {
        this.isLoading = true;
        this.structureList = await this.structureSvc.listStructure();
        this.isLoading = false;
    }

    compareThematic(thematic1: Thematic, thematic2: Thematic) {
        return thematic1 && thematic2 ? thematic1.id === thematic2.id : thematic1 === thematic2;
    }

    compareActivity(activity1: Thematic, activity2: Thematic) {
        return activity1 && activity2 ? activity1.id === activity2.id : activity1 === activity2;
    }

    compareStruct(struct1: StructurePlay, struct2: StructurePlay) {
        return struct1 && struct2 ? struct1.id === struct2.id : struct1 === struct2;
    }

    compareYear(year1: Thematic, year2: Thematic) {
        return year1 === year2;
    }

    compareDuration(duration1: number, duration2: number) {
        return duration1 === duration2;
    }

  
    async search() {
        this.isLoading = true;
        this.searchKeys.stayCity = this.stayCity ? this.stayCity : null;
        sessionStorage.setItem("LASTSEARCH", JSON.stringify(this.searchKeys));
        var search = await this.staySvc.stayFilters(this.searchKeys);
        sessionStorage.setItem("LASTSTAYLIST", JSON.stringify(search));
        this.staysList.emit(search);
        this.router.navigate(["stays-list"])
        this.isLoading = false;
    }

    changeStayCityPeriod(place: PlaceSuggestion) {
        this.stayCity = place.data.city;
    }

   

    
    

}
