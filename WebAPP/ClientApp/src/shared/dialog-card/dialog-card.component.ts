import { Component, Injectable, Inject, NgModule, ElementRef, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { AnonymousService } from '../../services/anonymous-service';
import { UtilsFunctions } from '../functions/utils-functions';
import { ErrorModalComponent } from '../error-modal/error-modal.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ErrorMessage } from '../../dto/errorClass/error';
import { User } from '../../dto/user';
import { CreationObject } from '../functions/creation-object';
import { UserService } from '../../services/user-service';
import { StructureService } from '../../services/structure-service';
import { Stay } from '../../dto/stay';
import { StayService } from '../../services/stay-service';
import { Structure } from '../../dto/structure';
import { Activity } from '../../dto/activity';
import { ActivityService } from '../../services/activity-service';
import { MatAutocompleteSelectedEvent, MatAutocomplete } from '@angular/material/autocomplete';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { ActivityStayService } from '../../services/activity-stay-service';
import { Thematic } from 'src/dto/thematic';
import { ThematicService } from 'src/services/thematic-service';
import { ThematicStayService } from 'src/services/thematic-stay-service';
import { Tag } from 'src/dto/tag';
import { TagService } from 'src/services/tag-service copy';
import { TagStayService } from 'src/services/tag-stay-service';

export interface DialogData {
    item: string;
    connexion: { profil: boolean, email: string, password: string }
    currentUser: User;
    currentStructure: Structure;
    stay: Stay;
}

@Component({
    selector: 'dialog-card',
    templateUrl: './dialog-card.component.html',
    styleUrls: ['./dialog-card.component.scss']
})


@Injectable()
export class DialogCardComponent {
    isLoading = false;

    emailFormControl = new FormControl('', [Validators.required, Validators.email]);
    passwordFormControl = new FormControl('', [Validators.required]);
    firstNameFormControl = new FormControl('', [Validators.required]);
    lastNameFormControl = new FormControl('', [Validators.required]);
    streetFormControl = new FormControl('', [Validators.required]);
    zipCodeFormControl = new FormControl('', [Validators.required]);
    cityFormControl = new FormControl('', [Validators.required]);
    countryFormControl = new FormControl('', [Validators.required]);
    nameFormControl = new FormControl('', [Validators.required]);
    siretFormControl = new FormControl('', [Validators.required]);

    isUser: boolean;
    newUser = new User();
    newStructure = new Structure();

    //Stay Form Control
    activityFirstFormGroup: FormGroup;

    activityList = new Array<Activity>();
    activitiesSelected = new Array<Activity>();
    activityCtrl = new FormControl();
    @ViewChild('activityInput', null) activityInput: ElementRef<HTMLInputElement>;
    filteredActivities: Observable<Array<Activity>>;

    thematicList = new Array<Thematic>();
    thematicsSelected = new Array<Thematic>();
    thematicCtrl = new FormControl();
    @ViewChild('thematicInput', null) thematicInput: ElementRef<HTMLInputElement>;
    filteredThematics: Observable<Array<Thematic>>;

    tagList = new Array<Tag>();
    tagsSelected = new Array<Tag>();

    constructor(@Inject(MAT_DIALOG_DATA) public data: DialogData, private anonymousSvc: AnonymousService, private structureSvc: StructureService,
        private _snackBar: MatSnackBar, private userSvc: UserService, public dialogRef: MatDialogRef<DialogCardComponent>, private staySvc: StayService,
        private acitivitySvc: ActivityService, private _formBuilder: FormBuilder, private activityStaySvc: ActivityStayService,
        private thematicSvc: ThematicService, private thematicStaySvc: ThematicStayService, private tagSvc: TagService,
        private tagStaySvc: TagStayService) {

        this.activityFirstFormGroup = this._formBuilder.group({
            stayNameFormControle: ['', [Validators.required]],
            stayDescirptionFormControle: ['', [Validators.required]],
            stayStartDateFormControle: ['', [Validators.required]],
            stayEndDateFormControle: ['', [Validators.required]],
            stayAbstractFormControle: ['', [Validators.required]],
            stayActivitiesFormControle: ['', [Validators.required]],
            stayStreetFormControle: ['', [Validators.required]],
            stayCityFormControle: ['', [Validators.required]],
            stayZipCodeFormControle: ['', [Validators.required]],
            stayCountryFormControle: ['', [Validators.required]],
            stayPriceFormControle: ['', [Validators.required]],
            stayOtherInformationsFormControle: [''],
            stayNbPlacesFormControle: [''],
            stayMinYearFormControle: [''],
            stayMaxYearFormControle: [''],
            stayPraticLeveltFormControle: [''],
        });
    }

    async ngOnInit() {
        if (this.data.item == "NewStay") {
            await this.loadActivitiesList()
            await this.loadThematicsList()
            await this.loadTagsList();

            if (this.data.stay.id != null) {
                await this.loadActivitiesOfStay();
                await this.loadThematicsOfStay();
                await this.loadTagsOfStay();
            }
        }

        this.filteredActivities = this.activityCtrl.valueChanges.pipe(
            startWith(null),
            map((activity: string | null) => activity ? this._filterActivity(activity) : this.activityList.slice()));

        this.filteredThematics = this.thematicCtrl.valueChanges.pipe(
            startWith(null),
            map((thematic: string | null) => thematic ? this._filterThematic(thematic) : this.thematicList.slice()));
    }

    async onClose() {
        this.isLoading = true;
        try {
            switch (this.data.item) {
                // case "Authentification":
                //     await this.connexion(this.data.connexion);
                //     break;
                case "UpdateParent":
                    this.updateUser(this.data.currentUser)
                    break;
                case "UpdateStructure":
                    this.updateStructure(this.data.currentStructure)
                    break;
                // case "NewStay":
                //     this.newStay();
                //     break;
                case "Registration":
                    await this.registration();
                    break;
            }
            this.dialogRef.close(this.data.item);
        }
        catch (ex) {
            this._snackBar.openFromComponent(ErrorModalComponent, {
                duration: 2000,
                data: new ErrorMessage(-1, ex.error.code)
            });
        }
        this.isLoading = false;

    }

    closeDialog() {
        this.dialogRef.close();
    }

    // async connexion(login: any) {
    //     await this.anonymousSvc.connexion(login.email, login.password)
    //     this._snackBar.openFromComponent(ErrorModalComponent, {
    //         duration: 2000,
    //         data: new ErrorMessage(1, "Connexion établie")
    //     });
    // }

    updateUser(u: User) {
        this.userSvc.update(u).then(rslt => {
            sessionStorage.setItem("USER", JSON.stringify(CreationObject.CreateUser(rslt)));
            var uUser = CreationObject.CreateUser(rslt);
            this._snackBar.openFromComponent(ErrorModalComponent, {
                duration: 2000,
                data: new ErrorMessage(1, "Modification(s) effectuée(s)")
            });

            this.dialogRef.close(uUser);
        })
            .catch(ex => {
                this._snackBar.openFromComponent(ErrorModalComponent, {
                    duration: 2000,
                    data: new ErrorMessage(-1, ex.error.code)
                });
            })
    }

    updateStructure(s: Structure) {
        this.structureSvc.update(s).then(rslt => {
            sessionStorage.setItem("STRUCTURE", JSON.stringify(CreationObject.CreateStructure(rslt)));
            var uStructure = CreationObject.CreateStructure(rslt);
            this._snackBar.openFromComponent(ErrorModalComponent, {
                duration: 2000,
                data: new ErrorMessage(1, "Modification(s) effectuée(s)")
            });

            this.dialogRef.close(uStructure);
        })
            .catch(ex => {
                this._snackBar.openFromComponent(ErrorModalComponent, {
                    duration: 2000,
                    data: new ErrorMessage(-1, ex.error.code)
                });
            })
    }

    // async newStay() {
    //     // await this.anonymousSvc.getLocation(this.data.stay.street, this.data.stay.zipCode).then(l => {
    //     //     this.data.stay.longitude = l.longitude;
    //     //     this.data.stay.latitude = l.latitude;
    //     //     this.data.stay.department = l.department;
    //     //     this.data.stay.region = l.region;
    //     // })

    //     if (this.data.stay.id != null) {
    //         this.staySvc.update(this.data.stay, this.activitiesSelected, this.thematicsSelected, this.tagsSelected).then(rslt => {
    //             this._snackBar.openFromComponent(ErrorModalComponent, {
    //                 duration: 2000,
    //                 data: new ErrorMessage(1, "Modification(s) effectuée(s)")
    //             });
    //             this.dialogRef.close();
    //         })
    //             .catch(ex => {
    //                 this._snackBar.openFromComponent(ErrorModalComponent, {
    //                     duration: 2000,
    //                     data: new ErrorMessage(-1, ex.error.code)
    //                 });
    //             })
    //     }
    //     else {
    //         this.staySvc.create(this.data.stay, this.activitiesSelected, this.thematicsSelected, this.tagsSelected).then(rslt => {
    //             this._snackBar.openFromComponent(ErrorModalComponent, {
    //                 duration: 2000,
    //                 data: new ErrorMessage(1, "Ajout effectué")
    //             });
    //             this.dialogRef.close();
    //         })
    //             .catch(ex => {
    //                 this._snackBar.openFromComponent(ErrorModalComponent, {
    //                     duration: 2000,
    //                     data: new ErrorMessage(-1, ex.error.code)
    //                 });
    //             })
    //     }

    // }

    async loadActivitiesList() {
        this.isLoading = true;
        this.activityList = await this.acitivitySvc.listActivity();
        this.isLoading = false;
    }

    async loadThematicsList() {
        this.isLoading = true;
        this.thematicList = await this.thematicSvc.listThematic();
        this.isLoading = false;
    }

    async loadTagsList() {
        this.isLoading = true;
        this.tagList = await this.tagSvc.listTag();
        this.isLoading = false;
    }

    async loadActivitiesOfStay() {
        this.isLoading = true;
        this.activitiesSelected = await this.activityStaySvc.listActivitiesOfStay(this.data.stay.id);
        this.isLoading = false;
    }

    async loadThematicsOfStay() {
        this.isLoading = true;
        this.thematicsSelected = await this.thematicStaySvc.listThematicsOfStay(this.data.stay.id);
        this.isLoading = false;
    }

    async loadTagsOfStay() {
        this.isLoading = true;
        this.tagsSelected = await this.tagStaySvc.listTagsOfStay(this.data.stay.id);
        this.isLoading = false;
    }

    removeActivity(activity: Activity): void {
        const index = this.activitiesSelected.indexOf(activity);

        if (index >= 0) {
            this.activitiesSelected.splice(index, 1);
        }
    }

    removeThematic(thematic: Thematic): void {
        const index = this.thematicsSelected.indexOf(thematic);

        if (index >= 0) {
            this.thematicsSelected.splice(index, 1);
        }
    }

    selectedActivity(event: MatAutocompleteSelectedEvent): void {
        var activity = this.activityList.find(activity => activity.label == event.option.viewValue);
        this.activitiesSelected.push(activity);
        this.activityInput.nativeElement.value = '';
        this.activityCtrl.setValue(null);
        this.activitiesSelected = this.distinctActivityArray(this.activitiesSelected);
    }

    selectedThematic(event: MatAutocompleteSelectedEvent): void {
        var thematic = this.thematicList.find(thematic => thematic.label == event.option.viewValue);
        this.thematicsSelected.push(thematic);
        this.thematicInput.nativeElement.value = '';
        this.thematicCtrl.setValue(null);
        this.thematicsSelected = this.distinctThematicArray(this.thematicsSelected);
    }

    private _filterActivity(value: string): Array<Activity> {
        const filterValue = value.toLowerCase();

        return this.activityList.filter(activity => activity.label.toLowerCase().indexOf(filterValue) === 0);
    }

    private _filterThematic(value: string): Array<Activity> {
        const filterValue = value.toLowerCase();

        return this.thematicList.filter(thematic => thematic.label.toLowerCase().indexOf(filterValue) === 0);
    }

    distinctActivityArray(array: Array<Activity>) {
        const result = new Array<Activity>();
        const map = new Map();

        for (const item of array) {
            if (!map.has(item)) {
                map.set(item, true);
                result.push(item);
            }
        }

        return result;
    }

    distinctThematicArray(array: Array<Thematic>) {
        const result = new Array<Thematic>();
        const map = new Map();

        for (const item of array) {
            if (!map.has(item)) {
                map.set(item, true);
                result.push(item);
            }
        }

        return result;
    }

    dayEnToFr(day: string) {
        return UtilsFunctions.dayEnToFr(day);
    }

    monthEnToFr(month: string) {
        return UtilsFunctions.monthEnToFr(month);
    }

    tagIsSelected(tag: Tag) {
        if (this.tagsSelected.find(t => t.id == tag.id) == undefined)
            return false;

        return true;
    }

    addTag(tag: Tag) {
        this.tagsSelected.push(tag);
    }

    removeTag(tag: Tag) {
        const index = this.tagsSelected.indexOf(tag);
        if (index >= 0) {
            this.tagsSelected.splice(index, 1);
        }
    }

    async registration() {
        this.isLoading = true;
        if (this.isUser) {
            await this.anonymousSvc.userRegistration(this.newUser);
        }
        else {
            await this.anonymousSvc.structureRegistration(this.newStructure);
        }

        this._snackBar.openFromComponent(ErrorModalComponent, {
            duration: 2000,
            data: new ErrorMessage(1, "Inscription réussie, vérifiez vos emails pour récupérer votre mot de passe")
        });
    }
}