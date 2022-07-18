import { Component, Injectable, EventEmitter, Output, Input } from '@angular/core';
import { DialogCardComponent } from '../../shared/dialog-card/dialog-card.component';
import { MatDialog } from '@angular/material/dialog';
import { DConnexion } from '../../dto/dialogClass/connexion';
import { isNullOrUndefined } from 'util';
import { Router } from '@angular/router';
import { User } from '../../dto/user';
import { UtilsFunctions } from '../../shared/functions/utils-functions';
import { GlobalService } from 'src/services/global-service';
import { Stay } from 'src/dto/stay';
import { AuthentificationDialog } from 'src/dialogs/authentification/authentification.dialog';
import { RegisterDialog } from 'src/dialogs/register/register.dialog';

@Component({
    selector: 'header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})

@Injectable()
export class HeaderComponent {
    currentUser: any;
    displayMenu = false;

    constructor(public dialog: MatDialog, private router: Router) { }

    ngOnInit() {
        this.currentUser = UtilsFunctions.InitUser();

        // if (this.currentUser == null)
        //     this.router.navigate(['home']);
    }

    logIn(): void {
        const dialogCard = this.dialog.open(AuthentificationDialog, {
            width: '70%',
            height: '80%',
            panelClass: 'mat-dialog-any-padding',
            autoFocus: false
        });

        dialogCard.afterClosed().subscribe(rslt => {
            if (!isNullOrUndefined(rslt))
                this.currentUser = UtilsFunctions.InitUser();
        });
    }

    register(): void{
        const dialogCard = this.dialog.open(RegisterDialog, {
            width: '80%',
            height: '90%',
            panelClass: 'mat-dialog-any-padding',
            autoFocus: false
        });

        dialogCard.afterClosed().subscribe(rslt => {
            if (!isNullOrUndefined(rslt))
                this.currentUser = UtilsFunctions.InitUser();
        });
    }

    // toRegister(): void {
    //     const dialogCard = this.dialog.open(AuthentificationDialogComponent, {
    //         width: '50%',
    //         height: '50%',
    //         data: { item: "Registration" },
    //         autoFocus: false
    //     });

    //     dialogCard.afterClosed().subscribe(rslt => {
    //     });
    // }

    logOut() {
        sessionStorage.setItem("TOKEN", null);
        sessionStorage.setItem("USER", null);
        sessionStorage.setItem("STRUCTURE", null);
        sessionStorage.setItem("HOMECATEGORY", "");
        this.router.navigate(["/"]);
        this.refreshUserConnected();
    }

    refreshUserConnected(){
        this.currentUser = UtilsFunctions.InitUser();
    }
}