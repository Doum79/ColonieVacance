import { Component, Injectable, OnInit } from '@angular/core';
import { Stay } from 'src/dto/stay';
import { StayService } from 'src/services/stay-service';
import { isNullOrUndefined } from 'util';

@Component({
    selector: 'stay-list',
    templateUrl: './stay-list.view.html',
    styleUrls: ['./stay-list.view.scss']
})

@Injectable()
export class StayListView {
    isLoading = false;

    staysList = new Array<Stay>();
    displayStyle = "BLOCK";

    constructor(public staySvc: StayService) {
    }

    async ngOnInit() {
        this.isLoading = true;
        if(!isNullOrUndefined(JSON.parse(sessionStorage.getItem("LASTSTAYLIST")))){
            this.staysList = JSON.parse(sessionStorage.getItem("LASTSTAYLIST"))
        }
        else{
            this.staysList = await this.staySvc.listStays();
        }
        this.isLoading = false;
    }

    updateList(list: Array<Stay>) {
        this.staysList = list;
    }
}