import { Component, Injectable, Input, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { Stay } from 'src/dto/stay';
import { StayFurtherInformation } from 'src/dto/stayFurtherInformation';
import { AnonymousService } from 'src/services/anonymous-service';

@Component({
    selector: 'stay-card',
    templateUrl: './stay-card.component.html',
    styleUrls: ['./stay-card.component.scss']
})


@Injectable()
export class StayCardComponent {

    @Input() stay: Stay;
    @Input() picture: string;
    @Input() redirection: boolean;

    price: number;

    constructor(private router: Router, private anonymousSvc: AnonymousService) { }

    ngOnInit() {}

    minimumPrice() {
        var min = this.stay.furtherInformationsList[0].price;
        if(this.stay.furtherInformationsList.length > 1){
            this.stay.furtherInformationsList.forEach(s => {
                if (min > s.price)
                min = s.price;
            })
        }
        return min;
    }

    goMoreInformations() {
        this.anonymousSvc.addViewToStay(this.stay.id);
        this.router.navigate(['stay/' + this.stay.id]);
    }
}