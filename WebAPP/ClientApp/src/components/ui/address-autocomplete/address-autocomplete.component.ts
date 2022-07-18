import { Component, Output, EventEmitter, OnDestroy, Injectable, Input } from '@angular/core';
import { MatOptionSelectionChange } from '@angular/material';
import { Subject, Subscription } from 'rxjs';
import { FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'address-autocomplete',
    templateUrl: './address-autocomplete.component.html',
    styleUrls: ['./address-autocomplete.component.scss']
})

@Injectable()
export class AddressAutoCompleteComponent {

  @Input() placeHolder: string;
  @Input() searchType: string;
  @Input() style: string;
  @Input() required: boolean;
  
  @Output()
    locationChange: EventEmitter<PlaceSuggestion> = new EventEmitter<PlaceSuggestion>();
  
    searchOptions: Subject<PlaceSuggestion[]> = new Subject<PlaceSuggestion[]>();
    inputFieldFormControl: FormControl = new FormControl();
  
    private valueChangesSub: Subscription;
    private choosenOption: PlaceSuggestion;
  
    private userInputTimeout: number;
    private requestSub: Subscription;
  
    constructor(private http: HttpClient) {
      this.valueChangesSub = this.inputFieldFormControl.valueChanges.subscribe((value) => {
        if (this.userInputTimeout) {
          window.clearTimeout(this.userInputTimeout);
        }
  
        if (this.choosenOption && this.choosenOption.shortAddress === value) {
          this.searchOptions.next(null);
          return;
        }
  
        if (!value || value.length < 3) {
          // do not need suggestions until for less than 3 letters
          this.searchOptions.next(null);
          return;
        }
  
        this.userInputTimeout = window.setTimeout(() => {
          this.generateSuggestions(value);
        }, 300);
      });
    }
  
    ngOnDestroy() {
      this.valueChangesSub.unsubscribe();
    }
  
    private generateSuggestions(text: string) {
      var url = `https://api.geoapify.com/v1/geocode/autocomplete?text=${text}&limit=5&apiKey=${environment.geoapifyApiKey}`;
      if (this.searchType) {
        url += `&type=${this.searchType}`;
    }


      if (this.requestSub) {
        this.requestSub.unsubscribe();
      }
  
      this.requestSub = this.http.get(url).subscribe((data: GeoJSON.FeatureCollection) => {
        const placeSuggestions = data.features.map(feature => {
          const properties: GeocodingFeatureProperties = (feature.properties as GeocodingFeatureProperties);
  
          return {
            shortAddress: this.generateShortAddress(properties),
            fullAddress: this.generateFullAddress(properties),
            data: properties
          }
        });
  
        this.searchOptions.next(placeSuggestions.length ? placeSuggestions : null);
      }, err => {
        console.log(err);
      });
    }
  
    private generateShortAddress(properties: GeocodingFeatureProperties): string {
      let shortAddress = properties.name;
  
      if (!shortAddress && properties.street && properties.housenumber) {
        // name is not set for buildings
        shortAddress = `${properties.street} ${properties.housenumber}`;
      }
  
      shortAddress += (properties.postcode && properties.city) ? `, ${properties.postcode}-${properties.city}`: '';
      shortAddress += (!properties.postcode && properties.city && properties.city  !== properties.name) ? `, ${properties.city}`: '';
      shortAddress += (properties.country && properties.country !== properties.name) ? `, ${properties.country}` : '';
  
      return shortAddress;
    }
  
    private generateFullAddress(properties: GeocodingFeatureProperties): string {
      let fullAddress = properties.name;
      fullAddress += properties.street ? `, ${properties.street}` : '';
      fullAddress += properties.housenumber ? ` ${properties.housenumber}` : '';
      fullAddress += (properties.postcode && properties.city) ? `, ${properties.postcode}-${properties.city}`: '';
      fullAddress += (!properties.postcode && properties.city && properties.city  !== properties.name) ? `, ${properties.city}`: '';
      fullAddress += properties.state ? `, ${properties.state}`: '';
      fullAddress += (properties.country && properties.country !== properties.name) ? `, ${properties.country}` : '';
      return fullAddress;
    }
  
    public optionSelectionChange(option: PlaceSuggestion, event: MatOptionSelectionChange) {
      if (event.isUserInput) {
        this.choosenOption = option;
        this.locationChange.emit(option);
      }
    }

    checkValue(option: any){
      if(this.searchType == "city")
        return `${option.data.city}, ${option.data.state}, ${option.data.country}`;
      
        return `${option.data.housenumber} ${option.data.street}, ${option.data.postcode} ${option.data.city}, ${option.data.country}`;
    }
  }
  
  export interface PlaceSuggestion {
    shortAddress: string;
    fullAddress: string;
    data: GeocodingFeatureProperties;
  }
  
  interface GeocodingFeatureProperties {
    name: string;
    country: string;
    state: string;
    postcode: string;
    city: string;
    address_line1: string;
    housenumber: string;
    lat: string;
    lon: string;
    street: string;
  }