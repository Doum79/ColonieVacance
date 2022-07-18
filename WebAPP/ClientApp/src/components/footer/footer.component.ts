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
import { Structure } from 'src/dto/structure';

@Component({
    selector: 'footer',
    templateUrl: './footer.component.html',
    styleUrls: ['./footer.component.scss']
})

@Injectable()
export class FooterComponent {
    currentUser: any;

    constructor(public dialog: MatDialog, private router: Router) { }

    ngOnInit() {
        this.currentUser = UtilsFunctions.InitUser();
    }



    refreshUserConnected(){
        this.currentUser = UtilsFunctions.InitUser();
    }

    addStay() {
        if (this.currentUser == null) {
            var data = new DConnexion(null, null, null);
            const dialogCard = this.dialog.open(DialogCardComponent, {
                width: '40%',
                data: { item: "Authentification", connexion: data }
            });
            dialogCard.afterClosed().subscribe(async rslt => {
                if (!isNullOrUndefined(rslt)){
                    this.currentUser = UtilsFunctions.InitUser();
                    
                    if (this.currentUser.profil == Structure.structureProfil) {
                        this.router.navigate(["edit-stay"]);
                    }
                }
            });
        }

        if (this.currentUser.profil == Structure.structureProfil) {
            this.router.navigate(["edit-stay"]);
        }
    }
}