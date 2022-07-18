import { Component, Injectable, ViewChild, ElementRef, OnInit, Input, SimpleChanges, EventEmitter, Output } from "@angular/core";
import { Router } from "@angular/router";
import * as L from 'leaflet';
import { Stay } from "src/dto/stay";
import { environment } from "src/environments/environment";
import { isNullOrUndefined } from "util";
import { User } from "../../../dto/user";

import { StayFurtherInformation } from 'src/dto/stayFurtherInformation';
import { AuthentificationDialog } from "../../../dialogs/authentification/authentification.dialog";
import { UtilsFunctions } from "../../../shared/functions/utils-functions";
import { MatDialog } from "@angular/material";

@Component({
    selector: 'map-item',
    templateUrl: './map-item.component.html',
    styleUrls: ['./map-item.component.scss']
})

@Injectable()
export class MapItemComponent {
    selected: null;
    hide = true;
    color ='red';
    hidden = false;

    @Input() inputStay: Stay;
    @Input() stay: Stay;

    @Output() currentUserParent: EventEmitter<any> = new EventEmitter<any>();
    @Input() inputCurrentUser: any;
    menuSelected: string;
    

    constructor(public dialog: MatDialog, private router: Router) { }


    ngOnInit() {
    }


    addFavoris(){
        this.hidden = !this.hidden;
    }

    minimumPrice() {
        var min = this.stay.furtherInformationsList[0].price;
        if (this.stay.furtherInformationsList.length > 1) {
            this.stay.furtherInformationsList.forEach(s => {
                if (min > s.price)
                    min = s.price;
            })
        }
        return min;
    }


    popFavoris() {
        if (this.inputCurrentUser == null) {
            const dialogfavorite = this.dialog.open(AuthentificationDialog, {
                width: '70%',
                height: '80%',
                panelClass: 'mat-dialog-any-padding',
                autoFocus: false
            });
            dialogfavorite.afterClosed().subscribe(async rslt => {
                if (!isNullOrUndefined(rslt)) {
                    this.inputCurrentUser = UtilsFunctions.InitUser();
                    this.currentUserParent.emit(this.inputCurrentUser);

                }
            });
        }

        if (this.inputCurrentUser.profil != null) {
           // this.router.navigate(["edit-stay"]);
           // this.color = favorite.selected;
        }
    }
    showStay(){
        this.router.navigate([`stay/${this.inputStay.id}`])
    }
}
