import { ValueConverter } from '@angular/compiler/src/render3/view/template';
import { Component, EventEmitter, Inject, Injectable, Input, Output } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material';
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
import { SearchdialogComponent } from '../searchdialog/searchdialog.component';
import { SelctDesComponent } from '../selct-des/selct-des.component';




@Component({
    selector: 'searchbar',
    templateUrl: './searchbar.component.html',
    styleUrls: ['./searchbar.component.scss']
})


@Injectable()
export class SearchbarComponent {

   
    isLoading = false;

    @Input() imageUrl: string;
    @Input() title: string;
    @Input() heightSize: string;
    @Output() staysList = new EventEmitter<Array<Stay>>();
   // @Input() typesOfMois: any;

    moreFilters = false;

    thematicList = new Array<Thematic>();
    yearList = ["2-8", "9-12", "13-16", "17-20", "21-30"];
    durationList = [
        { label: "2 jours", value: 2 },
        { label: "5 jours", value: 5 },
        { label: "1 semaine", value: 7 },
        { label: "2 semaines", value: 14 },
        { label: "3 semaines", value: 21 },
        { label: "1 mois", value: 31 },
        { label: "2 mois", value: 62 },
    ];
 
    
    activityList = new Array<Activity>();
    structureList = new Array<StructurePlay>();

    searchKeys: StayFilters;
    stayCity: string;

    typesOfMois :  {value?: number,label?: string} = {};
    
  
   
   

   
    

    constructor(private thematicSvc: ThematicService, private router: Router,
        private staySvc: StayService, private activitySvc: ActivityService, private structureSvc: StructureService, public dialog: MatDialog, public dialog2: MatDialog,) { }

    async ngOnInit() {
        this.searchKeys = !isNullOrUndefined(JSON.parse(sessionStorage.getItem("LASTSEARCH"))) ? JSON.parse(sessionStorage.getItem("LASTSEARCH")) : new StayFilters();
        await this.loadThematicsList()
        await this.loadActivityList()
        await this.loadstructureList()
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

    openDialog(): void {
        let dialogRef = this.dialog.open(SearchdialogComponent,  {
            height: '400px',
            width: '600px',
            position: {
                left: '20%',
                top: '9%',
               
            },
        
        });

        dialogRef.afterClosed().subscribe(result => {
           
            
            this.typesOfMois = result[0];
            this.searchKeys = result[0];
            
        });

    }


    openDialog2() {
        let dialogRef2 = this.dialog2.open(SelctDesComponent, {
            height: '400px',
            width: '600px',
            position: {
                left: '20%',
                top: '9%',

            }
        });

        dialogRef2.afterClosed().subscribe(result => {
            console.log(`Dialog result: ${result}`);
            
        });

    }
}


 
