import { Component, Injectable, Inject, NgModule, Input, Output, EventEmitter } from '@angular/core';
import { MatDialog } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthentificationDialog } from 'src/dialogs/authentification/authentification.dialog';
import { DConnexion } from 'src/dto/dialogClass/connexion';
import { Stay } from 'src/dto/stay';
import { Structure } from 'src/dto/structure';
import { isNullOrUndefined } from 'util';
import { DialogCardComponent } from '../../shared/dialog-card/dialog-card.component';
import { UtilsFunctions } from '../../shared/functions/utils-functions';

@Component({
    selector: 'menu',
    templateUrl: './menu.component.html',
    styleUrls: ['./menu.component.scss']
})


@Injectable()
export class MenuComponent {

    @Output() currentUserParent: EventEmitter<any> = new EventEmitter<any>();
    @Input() inputCurrentUser: any;
    menuSelected: string;

    menuItems = [
        { title: "Accueil", route: "/home" },
        { title: "Les séjours", route: "/stays-list" },
        { title: "Bons plans", route: "/" },
        { title: "Dernières minutes", route: "/" }
    ]

    constructor(public dialog: MatDialog, private router: Router) {

    }

    ngOnInit() {
        var currentUrl = window.location.href;
        this.menuSelected = "/" + currentUrl.split("/")[(currentUrl.split("/")).length - 1];

        this.inputCurrentUser = UtilsFunctions.InitUser();
    }

    addStay() {
        if (this.inputCurrentUser == null) {
            const dialogCard = this.dialog.open(AuthentificationDialog, {
                width: '70%',
                height: '80%',
                panelClass: 'mat-dialog-any-padding',
                autoFocus: false
            });
            dialogCard.afterClosed().subscribe(async rslt => {
                if (!isNullOrUndefined(rslt)) {
                    this.inputCurrentUser = UtilsFunctions.InitUser();
                    this.currentUserParent.emit(this.inputCurrentUser);

                    if (this.inputCurrentUser.profil == Structure.structureProfil) {
                        this.router.navigate(["edit-stay"]);
                    }
                }
            });
        }

        if (this.inputCurrentUser.profil == Structure.structureProfil) {
            this.router.navigate(["edit-stay"]);
        }
    }
}