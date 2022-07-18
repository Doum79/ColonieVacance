import { Injectable, Component, Inject, Output, EventEmitter } from "@angular/core";
import { FormControl, Validators } from "@angular/forms";
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA } from "@angular/material";
import { PlaceSuggestion } from "src/components/ui/address-autocomplete/address-autocomplete.component";
import { ErrorMessage } from "src/dto/errorClass/error";
import { Structure } from "src/dto/structure";
import { User } from "src/dto/user";
import { environment } from "src/environments/environment";
import { AnonymousService } from "src/services/anonymous-service";
import { ErrorModalComponent } from "src/shared/error-modal/error-modal.component";
import { isNullOrUndefined } from "util";

@Component({
    selector: 'register-dialog',
    templateUrl: './register.dialog.html',
    styleUrls: ['./register.dialog.scss']
})


@Injectable()
export class RegisterDialog {
    isLoading = false;
    hide = "password";

    isUser: boolean
    user = new User();
    structure = new Structure();
    place: any;
    @Output() registerForm = new EventEmitter();

    firstNameFormControl = new FormControl('', [Validators.required]);
    lastNameFormControl = new FormControl('', [Validators.required]);
    emailFormControl = new FormControl('', [Validators.required]);
    passwordFormControl = new FormControl('', [Validators.required]);
    confirmPasswordFormControl = new FormControl('', [Validators.required]);

    nameFormControl = new FormControl('', [Validators.required]);
    phoneNumberFormControl = new FormControl('', [Validators.required]);
    siretFormControl = new FormControl('', [Validators.required]);

    constructor(public dialog: MatDialogRef<RegisterDialog>, private _snackBar: MatSnackBar, private anonymousSvc: AnonymousService) {
    }

    ngOnInit() {

    }

    async register() {
        if (!this.checkForm())
            return;

        this.isLoading = true;
        try {
            if(this.isUser){
                await this.anonymousSvc.userRegistration(this.user);
            }
            else{
                await this.anonymousSvc.structureRegistration(this.structure);
            }
            this.dialog.close();
        }
        catch (ex) {
            this._snackBar.openFromComponent(ErrorModalComponent, {
                duration: environment.errorDuration,
                data: new ErrorMessage(-1, ex.error.code)
            });
        }
        this.isLoading = false;
    }

    changeStructureAddress(place: PlaceSuggestion) {
        this.place = place;
        this.structure.street = `${place.data.housenumber} ${place.data.street}`;
        this.structure.postCode = place.data.postcode;
        this.structure.city = place.data.city;
        this.structure.state = place.data.state;
        this.structure.country = place.data.country;
        this.structure.latitude = place.data.lat.toString();
        this.structure.longitude = place.data.lon.toString();
    }

    checkForm() {
        if (this.isUser) {
            if (this.firstNameFormControl.valid && this.lastNameFormControl.valid
                && this.emailFormControl.valid && this.passwordFormControl.valid
                && this.confirmPasswordFormControl.valid) {
                if (this.user.confirmPassword == this.user.password) {
                    return true;
                }
                else {
                    this._snackBar.openFromComponent(ErrorModalComponent, {
                        duration: environment.errorDuration,
                        data: new ErrorMessage(-1, "Vos mots de passe ne correspondent pas")
                    });
                    return false;
                }
            }
            else {
                this._snackBar.openFromComponent(ErrorModalComponent, {
                    duration: environment.errorDuration,
                    data: new ErrorMessage(-1, "Merci de remplir tous les champs")
                });
                return false;
            }
        }
        else {
            if (this.nameFormControl.valid && this.emailFormControl.valid
                && !isNullOrUndefined(this.place) && this.phoneNumberFormControl.valid
                && this.siretFormControl.valid && this.passwordFormControl.valid 
                && this.confirmPasswordFormControl.valid) {
                if (this.structure.confirmPassword == this.structure.password) {
                    return true;
                }
                else {
                    this._snackBar.openFromComponent(ErrorModalComponent, {
                        duration: environment.errorDuration,
                        data: new ErrorMessage(-1, "Vos mots de passe ne correspondent pas")
                    });
                    return false;
                }
            }
            else {
                this._snackBar.openFromComponent(ErrorModalComponent, {
                    duration: environment.errorDuration,
                    data: new ErrorMessage(-1, "Merci de remplir tous les champs")
                });
                return false;
            }
        }
    }
}