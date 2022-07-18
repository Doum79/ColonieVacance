import { Component, ElementRef, Injectable } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Stay } from 'src/dto/stay';
import { StayFurtherInformation } from 'src/dto/stayFurtherInformation';
import { StayService } from 'src/services/stay-service';

@Component({
    selector: 'stay-information',
    templateUrl: './stay-information.view.html',
    styleUrls: ['./stay-information.view.scss']
})

@Injectable()
export class StayInformationView {
    stay: Stay;
    fromPrice: number;

    isFirstCategory = true;
    selectedPicture: string;
    stayInArray = new Array<Stay>();

    constructor(private route: ActivatedRoute, private staySvc: StayService, private myElement: ElementRef) {
    }

    async ngOnInit() {
        var id = this.route.snapshot.paramMap.get('stayId');
        await this.loadingStay(parseInt(id));
        this.fromPrice = this.minimumPrice(this.stay.furtherInformationsList);
        this.selectedPicture = this.stay.picturesList[0].pictureUrl;
    }

    async loadingStay(id: number){
        this.stay = await this.staySvc.getStay(id);
        this.stayInArray.push(this.stay);
    }

    minimumPrice(list: Array<StayFurtherInformation>): number{
        var min = list[0].price;

        list.forEach( s => {
            if(min > s.price)
                min = s.price;
        })

        return min
    }

    scrollToElement(element: string){
        document.getElementById(element).scrollIntoView({ behavior: 'smooth' });
    }
}