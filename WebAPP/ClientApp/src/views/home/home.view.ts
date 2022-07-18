import { Component, Injectable } from '@angular/core';
import { User } from '../../dto/user';
import { StayService } from '../../services/stay-service';
import { Stay } from '../../dto/stay';
import { CreationObject } from '../../shared/functions/creation-object';
import { MatTableDataSource } from '@angular/material/table';
import { DialogCardComponent } from '../../shared/dialog-card/dialog-card.component';
import { MatDialog } from '@angular/material/dialog';
import { isNullOrUndefined } from 'util';
import { UtilsFunctions } from '../../shared/functions/utils-functions';
import { Router } from '@angular/router';
import { Structure } from '../../dto/structure';
import { StructureService } from '../../services/structure-service';
import { DConnexion } from '../../dto/dialogClass/connexion';
import { ErrorModalComponent } from '../../shared/error-modal/error-modal.component';
import { ErrorMessage } from '../../dto/errorClass/error';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormControl } from '@angular/forms';
import { COMMA, ENTER, SPACE, SEMICOLON } from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material/chips';
import { StayFilters } from '../../dto/parseClass/stayFilters';
import { AnonymousService } from 'src/services/anonymous-service';
import { OnInit } from '@angular/core';

@Component({
    selector: 'home',
    templateUrl: './home.view.html',
    styleUrls: ['./home.view.scss']
})

@Injectable()
export class HomeView implements OnInit{
    
    currentUser: any;

    stays: Array<Stay>;
    structures: Array<Structure>;
    favoriteStays = new Array<Stay>();
    favoriteStructures = new Array<Structure>();

    selectedCategory = "STAYS";

    keywords: Array<string> = new Array<string>();
    keywordCtrl = new FormControl();
    separatorKeysCodes: number[] = [ENTER, COMMA];
    staysFiltered: Array<Stay> = new Array<Stay>();

    activeRemove = false;
    removeStaysList: Array<Stay> = new Array<Stay>();

    //Aprés refonte graphique
    isLoading = false;
    popularStays = Array<Stay>();
    lastMinutesStays = Array<Stay>();

    constructor(private staySvc: StayService, public dialog: MatDialog, private router: Router, 
        private structureSvc: StructureService, private _snackBar: MatSnackBar, private anonymousSvc: AnonymousService) { }

    async ngOnInit() {
        this.currentUser = UtilsFunctions.InitUser();
        await this.loadingPopularStays();
        await this.loadingLastMinutesStays();
        // if (this.currentUser != null && this.currentUser.profil == User.parentProfil) {
        //     this.loadFavoriteStays();
        //     this.loadFavoriteStructures();
        // }

        await this.loadStays();
        // this.staysFiltered = this.stays;
        // this.loadStructures();

        // if (sessionStorage.getItem("HOMECATEGORY") != "" && sessionStorage.getItem("HOMECATEGORY") != null) {
        //     this.selectedCategory = sessionStorage.getItem("HOMECATEGORY").toString();
        //     sessionStorage.setItem("HOMECATEGORY", "");
        // }
    }

    async loadingPopularStays(){
        this.isLoading = true;
        this.popularStays = await this.staySvc.popularStays();
        this.isLoading = false;
    }

    async loadingLastMinutesStays(){
        this.isLoading = true;
        this.lastMinutesStays = await this.staySvc.lastMinutesStays();
        this.isLoading = false;
    }

    navigate(route: string, staysList: Array<Stay>){
        sessionStorage.setItem("LASTSTAYLIST", JSON.stringify(staysList));
        this.router.navigate([route]);
    }

    async loadStays() {
        this.isLoading = true;
        this.stays = new Array<Stay>();

        if (this.currentUser != null && this.currentUser.profil == Structure.structureProfil) {
            this.stays = await this.staySvc.listStaysByStructure(this.currentUser.id);
            this.staysFiltered = this.stays;
        }
        else {
            this.stays = await this.staySvc.listStays();
            this.staysFiltered = this.stays;
        }
        this.isLoading = false;
    }

    // loadStructures() {
    //     this.structures = new Array<Structure>();
    //     this.structureSvc.listStructure().then(st => {
    //         var tab = st as string[];
    //         tab.forEach(structure => {
    //             this.structures.push(CreationObject.CreateStructure(structure));
    //         })
    //     })
    // }

    // loadFavoriteStays() {
    //     this.favoriteStays = new Array<Stay>();
    //     this.staySvc.favoriteStaysList().then(rslt => {
    //         var tab = rslt as string[];

    //         tab.forEach(stay => {
    //             this.favoriteStays.push(CreationObject.CreateStay(stay));
    //         })
    //     })
    // }

    // loadFavoriteStructures() {
    //     this.favoriteStructures = new Array<Structure>();
    //     this.structureSvc.favoriteStructuresList().then(rslt => {
    //         var tab = rslt as string[];

    //         tab.forEach(structure => {
    //             this.favoriteStructures.push(CreationObject.CreateStructure(structure));
    //         })
    //     })
    // }

    // openStay(stay: Stay) {
    //     if (isNullOrUndefined(stay))
    //         stay = new Stay();

    //     if(stay.id != null)
    //         this.anonymousSvc.addViewToStay(stay.id);

    //     const dialogCard = this.dialog.open(DialogCardComponent, {
    //         width: '80%',
    //         data: { item: "NewStay", stay: stay },
    //         maxWidth: "",
    //         maxHeight: "90vh"
    //     });

    //     dialogCard.afterClosed().subscribe(async rslt => {
    //         (rslt);
    //         if (!isNullOrUndefined(rslt))
    //             await this.loadStays();
    //     });
    // }

    // addStayToFavorite(stayId: number) {
    //     if (this.currentUser == null) {
    //         sessionStorage.setItem("HOMECATEGORY", "STAYS");
    //         var data = new DConnexion(null, null, null);
    //         const dialogCard = this.dialog.open(DialogCardComponent, {
    //             width: '30%',
    //             data: { item: "Authentification", connexion: data }
    //         });

    //         dialogCard.afterClosed().subscribe(rslt => {
    //             if (!isNullOrUndefined(rslt)) {
    //                 if (rslt.profil) {
    //                     this.staySvc.addToFavorite(stayId).then(() => {
    //                         window.location.reload();
    //                     })
    //                 }
    //                 else {
    //                     this._snackBar.openFromComponent(ErrorModalComponent, {
    //                         duration: 2000,
    //                         data: new ErrorMessage(0, "En tant que structure, vous ne pouvez pas ajouter cette élément en favori")
    //                     });
    //                     window.location.reload();
    //                 }
    //             }
    //         });
    //     }
    //     else {
    //         this.staySvc.addToFavorite(stayId).then(() => {
    //             this.loadFavoriteStays();
    //             this.loadStays();
    //         })
    //     }
    // }

    // removeStayToFavorite(stayId: number) {
    //     this.staySvc.removeToFavorite(stayId).then(rslt => {
    //         this.loadFavoriteStays();
    //         this.loadStays();
    //     })
    // }

    // addStructureToFavorite(structureId: number) {
    //     if (this.currentUser == null) {
    //         sessionStorage.setItem("HOMECATEGORY", "STRUCTURES");
    //         var data = new DConnexion(null, null, null);
    //         const dialogCard = this.dialog.open(DialogCardComponent, {
    //             width: '30%',
    //             data: { item: "Authentification", connexion: data }
    //         });

    //         dialogCard.afterClosed().subscribe(rslt => {
    //             if (!isNullOrUndefined(rslt)) {
    //                 if (rslt.profil) {
    //                     this.structureSvc.addToFavorite(structureId).then(() => {
    //                         window.location.reload();
    //                     })
    //                 }
    //                 else {
    //                     this._snackBar.openFromComponent(ErrorModalComponent, {
    //                         duration: 2000,
    //                         data: new ErrorMessage(0, "En tant que structure, vous ne pouvez pas ajouter cette élément en favori")
    //                     });
    //                     window.location.reload();
    //                 }
                    
    //             }
    //         });
    //     }
    //     else {
    //         this.structureSvc.addToFavorite(structureId).then(() => {
    //             this.loadFavoriteStructures();
    //             this.loadStructures();
    //         })
    //     }
    // }

    // removeStructureToFavorite(structureId: number) {
    //     this.structureSvc.removeToFavorite(structureId).then(rslt => {
    //         this.loadFavoriteStructures();
    //         this.loadStructures();
    //     })
    // }

    // inStaysFavoriteList(stayId: number) {
    //     var stay = this.favoriteStays.find(s => s.id == stayId);

    //     if (stay != null)
    //         return true;

    //     return false;
    // }

    // inStructuresFavoriteList(structureId: number) {
    //     var structure = this.favoriteStructures.find(st => st.id == structureId);

    //     if (structure != null)
    //         return true;

    //     return false;
    // }

    // switchCategory(item: string) {
    //     this.selectedCategory = item;
    // }

    // openMap() {
    //     const dialogCard = this.dialog.open(DialogCardComponent, {
    //         width: '80%',
    //         data: { item: "Maps" }
    //     });

    //     dialogCard.afterClosed();
    // }

    // add(event: MatChipInputEvent): void {
    //     const input = event.input;
    //     const value = event.value;

    //     if ((value || '').trim()) {
    //         this.keywords.push(value.trim());
    //     }

    //     if (input) {
    //         input.value = '';
    //     }

    //     this.keywordCtrl.setValue(null);
    // }

    // remove(keyword: string): void {
    //     const index = this.keywords.indexOf(keyword);

    //     if (index >= 0) {
    //         this.keywords.splice(index, 1);
    //     }

    //     if (this.keywords.length == 0) {
    //         this.staysFiltered = this.stays;
    //         return;
    //     }
    // }

    // async search() {
    //     if (this.keywords.length == 0) {
    //         this.staysFiltered = this.stays;
    //         return;
    //     }
    //     var filters = new StayFilters();
    //     filters.keywords = this.keywords;
    //     this.staysFiltered = await this.staySvc.stayFilters(filters);
    // }

    // removeSelection(){
    //     this.activeRemove = !this.activeRemove;
    //     this.removeStaysList = new Array<Stay>();
    // }

    // addStayToRemove(stay: Stay){
    //     if(this.activeRemove){
    //         if(!this.checkStayRemoveSelected(stay)){
    //             this.removeStaysList.push(stay);
    //         }
    //         else{
    //             var index = this.removeStaysList.indexOf(stay);
    //             this.removeStaysList.splice(index, 1);
    //         }
    //     }
            
    // }

    // checkStayRemoveSelected(stay: Stay){
    //     var selectedStay = this.removeStaysList.find(s => s == stay);
    //     if(selectedStay != null)
    //         return true;
        
    //     return false;
    // }

    // async deleteStaysSelection(){
    //     this.isLoading = true
    //     await this.staySvc.remove(this.removeStaysList);
    //     await this.loadStays();
    //     this.removeSelection();
    //     this.isLoading = false;
    // }
}