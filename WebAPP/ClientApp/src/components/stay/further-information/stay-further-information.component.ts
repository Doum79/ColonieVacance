import { Component, Injectable, Input } from '@angular/core';
import { StayFurtherInformation } from 'src/dto/stayFurtherInformation';

@Component({
    selector: 'stay-further-information',
    templateUrl: './stay-further-information.component.html',
    styleUrls: ['./stay-further-information.component.scss']
})


@Injectable()
export class StayFurtherInformationComponent {

    @Input() furtherInformationsList: Array<StayFurtherInformation>;

    constructor() { }

    ngOnInit() {

    }

    calculateDiffDate(startDate: Date, endDate: Date) {
        var firstDate = new Date(startDate);
        var lastDate = new Date(endDate);

        return Math.floor(
            (Date.UTC(
                lastDate.getFullYear(), lastDate.getMonth(), lastDate.getDate()
            )
                -
                Date.UTC(
                    firstDate.getFullYear(), firstDate.getMonth(), firstDate.getDate()
                )) / (1000 * 60 * 60 * 24));
    }

    goTo(url: string){
        window.open(url, "_blank");
    }
}