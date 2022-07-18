import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpModule } from '@angular/http';
import { CommonModule } from '@angular/common';

//Material Module
import { MatSliderModule } from '@angular/material/slider';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatStepperModule } from '@angular/material/stepper';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatExpansionModule, MatTooltipModule } from '@angular/material';
import { MatSelectModule } from '@angular/material/select';
import { MatTabsModule } from '@angular/material/tabs';
import { MatListModule } from '@angular/material/list';
import {MatBadgeModule} from '@angular/material/badge';

//Material Module

//Others Modules
import { Ng2TelInputModule } from 'ng2-tel-input';
//Others Modules

//Component
import { HeaderComponent } from '../components/header/header.component';
import { MenuComponent } from '../components/menu/menu.component';
import { FavoriteComponent } from '../components/favorite/favorite.component';
import { StayCardComponent } from 'src/components/stay/card/stay-card.component';
import { StayFurtherInformationComponent } from 'src/components/stay/further-information/stay-further-information.component';
//Component

//Views
import { HomeView } from '../views/home/home.view';
import { AccountComponent } from '../views/account/account.component';
import { StayListView } from '../views/stay/list/stay-list.view';
import { EditStayView } from 'src/views/stay/edit/edit.view';
import { StayInformationView } from 'src/views/stay/information/stay-information.view';
//Views

//Shared Component
import { LoaderComponent } from '../shared/loader/loader.component';
import { DialogCardComponent } from '../shared/dialog-card/dialog-card.component';
import { ErrorModalComponent } from '../shared/error-modal/error-modal.component';
import { SearchbarComponent } from 'src/shared/searchbar/searchbar.component';
import { FooterComponent } from 'src/components/footer/footer.component';
import { SearchdialogComponent } from '../shared/searchdialog/searchdialog.component';
import { SelctDesComponent } from '../shared/selct-des/selct-des.component';
//Shared Component

//Dialog
import { AuthentificationDialog } from 'src/dialogs/authentification/authentification.dialog';
import { RegisterDialog } from 'src/dialogs/register/register.dialog';
//Dialog

//Utils
import { AddressAutoCompleteComponent } from 'src/components/ui/address-autocomplete/address-autocomplete.component';
//Utils

//Services
import { AnonymousService } from '../services/anonymous-service';
import { UserService } from '../services/user-service';
import { StructureService } from '../services/structure-service';
import { StayService } from '../services/stay-service';
import { ActivityService } from '../services/activity-service';
import { ActivityStayService } from '../services/activity-stay-service';
import { ThematicStayService } from 'src/services/thematic-stay-service';
import { ThematicService } from 'src/services/thematic-service';
import { TagService } from 'src/services/tag-service copy';
import { TagStayService } from 'src/services/tag-stay-service';
import { GlobalService } from 'src/services/global-service';
import { MapComponent } from 'src/components/map/map/map.component';
import { MapItemComponent } from 'src/components/map/item/map-item.component';

//Services

@NgModule({
    declarations: [
        AppComponent,
        LoaderComponent,
        HomeView,
        HeaderComponent,
        DialogCardComponent,
        ErrorModalComponent,
        AccountComponent,
        MapComponent,
        MenuComponent,
        FavoriteComponent,
        StayCardComponent,
        StayListView,
        SearchbarComponent,
        EditStayView,
        AddressAutoCompleteComponent,
        FooterComponent,
        AuthentificationDialog,
        StayInformationView,
        StayFurtherInformationComponent,
        RegisterDialog,
        MapItemComponent,
        SearchdialogComponent,
        SelctDesComponent,
       
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        FormsModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        MatSliderModule,
        MatRadioModule,
        MatButtonModule,
        MatDialogModule,
        MatInputModule,
        MatFormFieldModule,
        ReactiveFormsModule,
        HttpModule,
        MatSnackBarModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatIconModule,
        MatChipsModule,
        MatStepperModule,
        MatAutocompleteModule,
        MatExpansionModule,
        MatSelectModule,
        CommonModule,
        MatTooltipModule,
        Ng2TelInputModule,
        RouterModule,
        MatTabsModule,
        MatListModule,
        MatBadgeModule
        
    ],
   
    entryComponents: [
        DialogCardComponent,
        ErrorModalComponent,
        AuthentificationDialog,
        RegisterDialog,
        MapItemComponent,
        SearchdialogComponent,
        SelctDesComponent,
      

    ],
    providers: [
        AnonymousService,
        UserService,
        StructureService,
        StayService,
        ActivityService,
        MatDatepickerModule,
        ActivityStayService,
        ThematicStayService,
        ThematicService,
        TagService,
        TagStayService,
        GlobalService,
        { provide: MAT_DATE_LOCALE, useValue: 'fr-FR' }
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    bootstrap: [AppComponent]
})
export class AppModule {}
