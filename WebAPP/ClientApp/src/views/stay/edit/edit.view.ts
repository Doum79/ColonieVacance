import { isNull } from '@angular/compiler/src/output/output_ast';
import { Component, ElementRef, Injectable, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatAutocompleteSelectedEvent, MatSnackBar } from '@angular/material';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map, startWith, switchMapTo } from 'rxjs/operators';
import { PlaceSuggestion } from 'src/components/ui/address-autocomplete/address-autocomplete.component';
import { Activity } from 'src/dto/activity';
import { ErrorMessage } from 'src/dto/errorClass/error';
import { Stay } from 'src/dto/stay';
import { StayAccess } from 'src/dto/stayAccess';
import { StayActivity } from 'src/dto/stayActivity';
import { StayEquipment } from 'src/dto/stayEquipment';
import { StayFurtherInformation } from 'src/dto/stayFurtherInformation';
import { StayPicture } from 'src/dto/stayPicture';
import { StayTeam } from 'src/dto/stayTeam';
import { StayThematic } from 'src/dto/stayThematic';
import { Thematic } from 'src/dto/thematic';
import { environment } from 'src/environments/environment';
import { ActivityService } from 'src/services/activity-service';
import { StayService } from 'src/services/stay-service';
import { ThematicService } from 'src/services/thematic-service';
import { ErrorModalComponent } from 'src/shared/error-modal/error-modal.component';
import { ModalMessage } from 'src/shared/functions/modal-message';
import { UtilsFunctions } from 'src/shared/functions/utils-functions';
import { isNullOrUndefined } from 'util';

@Component({
    selector: 'edit-stay',
    templateUrl: './edit.view.html',
    styleUrls: ['./edit.view.scss']
})

@Injectable()
export class EditStayView {
    // firstStepGroup: FormGroup;
    // secondStepGroup: FormGroup;
    stay: Stay;
    displayFormInvalid = false;

    isLoading = false;
    @ViewChild('imgInput', { static: false }) imageInput: ElementRef;
    model: File;

    thematicsList: Array<Thematic> = new Array<Thematic>();
    @ViewChild('thematicInput', null) thematicInput: ElementRef<HTMLInputElement>;
    thematicCtrl = new FormControl();
    filteredThematics: Observable<Array<Thematic>>;

    activitiesList: Array<Activity> = new Array<Activity>();
    @ViewChild('activityInput', null) activityInput: ElementRef<HTMLInputElement>;
    activityCtrl = new FormControl();
    filteredActivities: Observable<Array<Activity>>;

    partner: string = "";
    equipmentIncluded: string = "";
    equipmentNotIncluded: string = "";
    access: string = "";

    base64List = new Array<{ key: string, url: any }>();
    formData = new FormData();
    displayCard = false;

    constructor(private _formBuilder: FormBuilder, private staySvc: StayService, private _snackBar: MatSnackBar,
        private thematicSvc: ThematicService, private activitySvc: ActivityService, private router: Router) {
    }

    async ngOnInit() {
        this.stay = new Stay();
        this.stay.partnersList = new Array<StayTeam>();
        this.stay.equipmentsList = new Array<StayEquipment>();
        this.stay.accessesList = new Array<StayAccess>();
        this.stay.picturesList = new Array<StayPicture>();
        this.stay.furtherInformationsList = new Array<StayFurtherInformation>();
        this.stay.activitiesList = new Array<StayActivity>();
        this.stay.thematicsList = new Array<StayThematic>();
        this.addStayPeriod();

        // this.firstStepGroup = this._formBuilder.group({
        //     title: ['', Validators.required],
        //     minYear: ['', Validators.required],
        //     maxYear: ['', Validators.required],
        //     abstract: ['', Validators.required],
        //     description: ['', Validators.required],
        //     phone: ['', Validators.required],
        // });

        // this.secondStepGroup = this._formBuilder.group({
        //     startDate: ['', Validators.required],
        //     endDate: ['', Validators.required],
        //     withTransport: ['', Validators.required],
        //     price: ['', Validators.required]
        // });

        await this.loadingThematics();
        this.filteredThematics = this.thematicCtrl.valueChanges.pipe(
            startWith(null),
            map((thematic: string | null) => thematic ? this._filterThematic(thematic) : this.thematicsList.slice()));

        await this.loadingActivities();
        this.filteredActivities = this.activityCtrl.valueChanges.pipe(
            startWith(null),
            map((activity: string | null) => activity ? this._filterActivity(activity) : this.activitiesList.slice()));
    }

    async addStay() {
        this.isLoading = true;
        try{
            await this.staySvc.create(this.stay, this.formData);
            this._snackBar.openFromComponent(ErrorModalComponent, {
                duration: environment.errorDuration,
                data: new ErrorMessage(1, ModalMessage.AddStay())
            });
            this.router.navigate(["/"]);
        }
        catch (ex) {
            this._snackBar.openFromComponent(ErrorModalComponent, {
                duration: environment.errorDuration,
                data: new ErrorMessage(-1, ex.error.code)
            });
        }
        this.isLoading = false;
    }

    async addPicture(event) {
        this.isLoading = true;
        var file = event.target.files[0];

        if (file.size > environment.maxPictureSize) {
            this._snackBar.openFromComponent(ErrorModalComponent, {
                duration: environment.errorDuration,
                data: new ErrorMessage(-1, "Fichier trop volumineux (4Mo maximum)")
            });
            this.isLoading = false
            return;
        }

        var anyObject = { key: `picture${this.base64List.length}`, url: await UtilsFunctions.getBase64(file) };
        this.formData.append(`picture${this.base64List.length}`, file);
        this.base64List.push(anyObject);
        //this.model = this.imageInput.nativeElement.files[0];
        // if (this.model.size > environment.maxPictureSize) {
        //     this._snackBar.openFromComponent(ErrorModalComponent, {
        //         duration: environment.errorDuration,
        //         data: new ErrorMessage(-1, "Fichier trop volumineux (4Mo maximum)")
        //     });
        //     this.isLoading = false
        //     return;
        // }
        //var newPicture = new StayPicture(null, null, (await UtilsFunctions.getBase64(this.model)).toString(), this.model.name);
        //this.stay.picturesList.push(newPicture);
        this.isLoading = false;
    }

    async loadingThematics() {
        this.isLoading = true;
        this.thematicsList = await this.thematicSvc.listThematic();
        this.isLoading = false;
    }

    async loadingActivities() {
        this.isLoading = true;
        this.activitiesList = await this.activitySvc.listActivity();
        this.isLoading = false;
    }

    selectedThematic(choosenThematic: Thematic): void {
        var thematic = this.thematicsList.find(t => t.id == choosenThematic.id);
        this.stay.thematicsList.push(new StayThematic(null, null, null, thematic.id, thematic));
        this.thematicInput.nativeElement.value = '';
        this.thematicCtrl.setValue(null);
        this.stay.thematicsList = this.distinctThematicArray(this.stay.thematicsList);

        this.filteredThematics = this.thematicCtrl.valueChanges.pipe(
            startWith(null),
            map((thematic: string | null) => thematic ? this._filterThematic(thematic) : this.thematicsList.slice()));
    }

    selectedActivity(choosenActivity: Activity): void {
        var activity = this.activitiesList.find(a => a.id == choosenActivity.id);
        this.stay.activitiesList.push(new StayActivity(null, null, null, activity.id, activity));
        this.activityInput.nativeElement.value = '';
        this.activityCtrl.setValue(null);
        this.stay.activitiesList = this.distinctActivityArray(this.stay.activitiesList);

        this.filteredActivities = this.activityCtrl.valueChanges.pipe(
            startWith(null),
            map((activity: string | null) => activity ? this._filterActivity(activity) : this.activitiesList.slice()));
    }

    distinctThematicArray(array: Array<StayThematic>) {
        const result = new Array<StayThematic>();
        const map = new Map();

        for (const item of array) {
            if (!map.has(item)) {
                map.set(item, true);
                result.push(item);
            }
        }

        return result;
    }

    distinctActivityArray(array: Array<StayActivity>) {
        const result = new Array<StayActivity>();
        const map = new Map();

        for (const item of array) {
            if (!map.has(item)) {
                map.set(item, true);
                result.push(item);
            }
        }
        return result;
    }

    removeThematic(stayThematic: StayThematic): void {
        const index = this.stay.thematicsList.indexOf(stayThematic);

        if (index >= 0) {
            this.stay.thematicsList.splice(index, 1);
        }
    }

    removeActivity(stayActivity: StayActivity): void {
        const index = this.stay.activitiesList.indexOf(stayActivity);

        if (index >= 0) {
            this.stay.activitiesList.splice(index, 1);
        }
    }

    private _filterThematic(value: string): Array<Thematic> {
        const filterValue = value.toLowerCase();
        return this.thematicsList.filter(thematic => thematic.label.toLowerCase().indexOf(filterValue) === 0);
    }

    private _filterActivity(value: string): Array<Activity> {
        const filterValue = value.toLowerCase();
        return this.activitiesList.filter(activity => activity.label.toLowerCase().indexOf(filterValue) === 0);
    }

    addPartner(): void {
        if (isNullOrUndefined(this.partner) || this.partner == "")
            return

        this.stay.partnersList.push(new StayTeam(null, null, null, this.partner));
        this.partner = "";
    }

    removePartner(stayPartner: StayTeam): void {
        const index = this.stay.partnersList.indexOf(stayPartner);

        if (index >= 0) {
            this.stay.partnersList.splice(index, 1);
        }
    }

    addEquipment(isIncluded: boolean): void {
        if (
            (isNullOrUndefined(this.equipmentIncluded) || this.equipmentIncluded == "")
            &&
            (isNullOrUndefined(this.equipmentNotIncluded) || this.equipmentNotIncluded == "")
        )
            return

        if (isIncluded) {
            this.stay.equipmentsList.push(new StayEquipment(null, null, null, this.equipmentIncluded, isIncluded));
        }
        else {
            this.stay.equipmentsList.push(new StayEquipment(null, null, null, this.equipmentNotIncluded, isIncluded));
        }
        this.equipmentIncluded = "";
        this.equipmentNotIncluded = "";
    }

    removeEquimpent(stayEquipment: StayEquipment): void {
        const index = this.stay.equipmentsList.indexOf(stayEquipment);

        if (index >= 0) {
            this.stay.equipmentsList.splice(index, 1);
        }
    }

    addAccess(): void {
        if (isNullOrUndefined(this.access) || this.access == "")
            return

        this.stay.accessesList.push(new StayAccess(null, null, null, this.access));
        this.access = "";
    }

    removeAccess(stayAccess: StayAccess): void {
        const index = this.stay.accessesList.indexOf(stayAccess);

        if (index >= 0) {
            this.stay.accessesList.splice(index, 1);
        }
    }

    changeStayAddress(place: PlaceSuggestion) {
        this.stay.street = `${place.data.housenumber} ${place.data.street}`;
        this.stay.postCode = place.data.postcode;
        this.stay.city = place.data.city;
        this.stay.state = place.data.state;
        this.stay.country = place.data.country;
        this.stay.latitude = place.data.lat.toString();
        this.stay.longitude = place.data.lon.toString();
    }

    addStayPeriod() {
        this.stay.furtherInformationsList.push(new StayFurtherInformation());
    }

    changeStartCityPeriod(place: PlaceSuggestion, index: number) {
        this.stay.furtherInformationsList[index].startCity = place.data.city;
    }

    deletePeriod(index: number) {
        if (!isNullOrUndefined(this.stay.furtherInformationsList[index]))
            this.stay.furtherInformationsList.splice(index, 1);
    }

    getKeyEvent(event, inputName) {
        if (event.keyCode != 13)
            return

        switch (inputName) {
            case "TEAM":
                this.addPartner();
                break;
            case "EQUIPMENTINCLUDED":
                this.addEquipment(true);
                break;
            case "EQUIPMENTNOTINCLUDED":
                this.addEquipment(false);
                break;
            case "ACCESS":
                this.addAccess();
                break;
        }
    }
}