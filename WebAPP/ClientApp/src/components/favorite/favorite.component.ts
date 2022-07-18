import { Component, Injectable } from "@angular/core";
import { UtilsFunctions } from "../../shared/functions/utils-functions";
import { Router } from "@angular/router";
import { Stay } from "../../dto/stay";
import { CreationObject } from "../../shared/functions/creation-object";
import { StayService } from "../../services/stay-service";
import { StructureService } from "../../services/structure-service";
import { Structure } from "../../dto/structure";

@Component({
    selector: 'favorite',
    templateUrl: './favorite.component.html',
    styleUrls: ['./favorite.component.scss']
})

@Injectable()
export class FavoriteComponent {
    currentUser: any;

    selectedCategory = "STAYS"
    favoriteStays: Array<Stay>;
    favoriteStructures: Array<Structure>;

    constructor(private router: Router, private staySvc: StayService, private structureSvc: StructureService) { }

    ngOnInit() {
        this.currentUser = UtilsFunctions.InitUser();

        if (this.currentUser == null)
            this.router.navigate(['home']);

        this.loadFavoriteStays();
        this.loadFavoriteStructures();
    }

    switchCategory(item: string) {
        this.selectedCategory = item;
    }

    loadFavoriteStays() {
        this.favoriteStays = new Array<Stay>();
        this.staySvc.favoriteStaysList().then(rslt => {
            var tab = rslt as string[];

            tab.forEach(stay => {
                this.favoriteStays.push(CreationObject.CreateStay(stay));
            })
        })
    }

    loadFavoriteStructures() {
        this.favoriteStructures = new Array<Structure>();
        this.structureSvc.favoriteStructuresList().then(rslt => {
            var tab = rslt as string[];

            tab.forEach(structure => {
                this.favoriteStructures.push(CreationObject.CreateStructure(structure));
            })
        })
    }

    removeStayToFavorite(stayId: number) {
        this.staySvc.removeToFavorite(stayId).then(() => {
            this.loadFavoriteStays();
        })
    }

    removeStructureToFavorite(structureId: number) {
        this.structureSvc.removeToFavorite(structureId).then(() => {
            this.loadFavoriteStructures();
        })
    }
}