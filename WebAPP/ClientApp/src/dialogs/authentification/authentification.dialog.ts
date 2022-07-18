import { HttpErrorResponse } from "@angular/common/http";
import { Injectable, Component, Inject, Input, Output, EventEmitter } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { MatSnackBar, MAT_SNACK_BAR_DATA } from "@angular/material/snack-bar";
import { User } from "src/dto/user";
import { ErrorMessage } from "src/dto/errorClass/error";
import { environment } from "src/environments/environment";
import { AnonymousService } from "src/services/anonymous-service";
import { ErrorModalComponent } from "src/shared/error-modal/error-modal.component";

@Component({
    selector: 'authentification-dialog',
    templateUrl: './authentification.dialog.html',
    styleUrls: ['./authentification.dialog.scss']
})


@Injectable()
export class AuthentificationDialog {
   
    
   
    isLoading = false;
    hide = "password";
   registerForm = new User();
    email: string;
    password: string;

    emailFormControl = new FormControl('', [Validators.required]);
    passwordFormControl = new FormControl('', [Validators.required]);

    constructor(private _snackBar: MatSnackBar, public dialog: MatDialogRef<AuthentificationDialog>,
        private anonymousSvc: AnonymousService) {
    }

    ngOnInit() {

    }

    async connexion() {
        if(!this.checkForm()){
            this._snackBar.openFromComponent(ErrorModalComponent, {
                duration: environment.errorDuration,
                data: new ErrorMessage(-1, "Merci de remplir tous les champs")
            });
            return;
        }

        this.isLoading = true;
        try {
            var rslt = await this.anonymousSvc.connexion(this.email, this.password)
            this._snackBar.openFromComponent(ErrorModalComponent, {
                duration: environment.errorDuration,
                data: new ErrorMessage(1, "Connexion établie")
            });
            this.dialog.close(rslt);
        }
        catch(ex){
            this._snackBar.openFromComponent(ErrorModalComponent, {
                duration: environment.errorDuration,
                data: new ErrorMessage(-1, ex.error.code)
            });
        }
        this.isLoading = false;
    }

    checkForm(){
        if(!this.emailFormControl.valid || !this.passwordFormControl.valid)
            return false;
        return true;
    }
   
    register(form: User) {
        this.registerForm = form;
    }
    
}