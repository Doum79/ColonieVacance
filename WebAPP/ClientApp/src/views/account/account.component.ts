import { Component, Injectable } from "@angular/core";
import { User } from "../../dto/user";
import { DialogCardComponent } from "../../shared/dialog-card/dialog-card.component";
import { MatDialog } from "@angular/material/dialog";
import { isNullOrUndefined } from "util";
import { UtilsFunctions } from "../../shared/functions/utils-functions";
import { Router } from "@angular/router";

@Component({
    selector: 'account',
    templateUrl: './account.component.html',
    styleUrls: ['./account.component.scss']
})

@Injectable()
export class AccountComponent {
    currentUser: any;

    constructor(public dialog: MatDialog, private router: Router) { }

    ngOnInit() {
        this.currentUser = UtilsFunctions.InitUser();

        if (this.currentUser == null)
            this.router.navigate(['home']);
    }

    openDialogCard(item: string): void {
        const dialogCard = this.dialog.open(DialogCardComponent, {
            width: '30%',
            data: { item: item, currentStructure: this.currentUser }
        });

        dialogCard.afterClosed().subscribe(rslt => {
            if (!isNullOrUndefined(rslt)) {
                this.currentUser = rslt
            }
        });
    }
}