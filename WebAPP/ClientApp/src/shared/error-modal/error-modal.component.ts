import { Injectable, Component, Inject } from "@angular/core";
import { MAT_SNACK_BAR_DATA } from "@angular/material/snack-bar";

@Component({
    selector: 'error-modal',
    templateUrl: './error-modal.component.html',
    styleUrls: ['./error-modal.component.scss']
})


@Injectable()
export class ErrorModalComponent {

    constructor(@Inject(MAT_SNACK_BAR_DATA) public data: any) {

    }
}