import { Component, Injectable, ViewChild, ElementRef, OnInit, Input, ComponentFactoryResolver, Injector, ApplicationRef, SimpleChanges } from "@angular/core";
import "leaflet/dist/leaflet.css";
import * as L from 'leaflet';
import "esri-leaflet-geocoder/dist/esri-leaflet-geocoder.css";
import "esri-leaflet-geocoder/dist/esri-leaflet-geocoder";
import { GeoSearchControl, OpenStreetMapProvider } from 'leaflet-geosearch';
import * as ELG from "esri-leaflet-geocoder";

import { Stay } from "src/dto/stay";
import { environment } from "src/environments/environment";
import { isNullOrUndefined } from "util";
import { User } from "../../../dto/user";
import { MapItemComponent } from "../item/map-item.component";

@Component({
    selector: 'map',
    templateUrl: './map.component.html',
    styleUrls: ['./map.component.scss']
})

@Injectable()
export class MapComponent {
    currentUser: User;
    isLoading = false;
    marker;

    @Input() inputDisplayStays: Array<Stay>;
    @Input() inputToFocus: string;
    @Input() inputHeight: number;

    leafletMap: any;
    currentPositionIcon: L.Icon<L.IconOptions>;
    stayIcon: L.Icon<L.IconOptions>;

    constructor(private resolver: ComponentFactoryResolver, private injector: Injector,
        private appRef: ApplicationRef) {
    }


    async ngOnInit() {
        this.currentUser = JSON.parse(sessionStorage.getItem("USER"));
        this.loadingIcons();

        this.leafletMap = L.map('map').setView([47.003332, 2.409741], 6);
        L.tileLayer(
            `${environment.tilesMapUrl}${environment.maptilerApiKey}`,
            {
                attribution: environment.applicationName
            }
        )
            .addTo(this.leafletMap);

        await this.displayCurrentPosition();


        const searchControl = new ELG.Geosearch();
        const results = new L.LayerGroup().addTo(this.leafletMap);

        searchControl
            .on("results", function (data) {
                results.clearLayers();
                for (let i = data.results.length - 1; i >= 0; i--) {
                    results.addLayer(L.marker(data.results[i].latlng));
                }
            })
            .addTo(this.leafletMap);


        this.leafletMap.on("click", (e) => {
            new ELG.ReverseGeocode().latlng(e.latlng).run((error, result) => {
                if (error) {
                    return;
                }
                if (this.marker && this.leafletMap.hasLayer(this.marker))
                    this.leafletMap.removeLayer(L.marker);

                this.marker = L.marker(result.latlng)
                    .addTo(this.leafletMap)
                    .bindPopup(result.address.Match_addr)
                    .openPopup();
            });
        });

    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes.inputDisplayStays.currentValue.length > 0)
            this.displayStays();
    }

    loadingIcons() {
        this.currentPositionIcon = L.icon(
            {
                iconUrl: '../../assets/maps/home-location.png',
                iconSize: [30, 30]
            }
        );

        this.stayIcon = L.icon(
            {
                iconUrl: '../../assets/maps/stay-location.png',
                iconSize: [15, 15]
            }
        );
    }

    async displayCurrentPosition() {
        if (navigator.geolocation) {
            await navigator.geolocation.getCurrentPosition(p => {
                if (this.inputToFocus == "USER")
                    this.leafletMap.panTo(new L.LatLng(p.coords.latitude, p.coords.longitude));

                L.marker(
                    [p.coords.latitude, p.coords.longitude],
                    { icon: this.currentPositionIcon }
                )
                    .bindPopup('Ma position')
                    .addTo(this.leafletMap)
            });

        }
    }

    displayStays() {
        this.inputDisplayStays.forEach(s => {
            var popup = this.compilePopup(MapItemComponent,
                (c) => {
                    c.instance.inputStay = s
                })

            if (s.latitude && s.longitude)
                if (this.inputToFocus == "STAY")
                    this.leafletMap.panTo(new L.LatLng(+s.latitude + 2, +s.longitude));

            L.marker(
              
                [+s.latitude, +s.longitude],
                { icon: this.stayIcon }
            )
                .bindPopup(popup,
                    {
                        closeButton: false,
                        maxWidth: 450
                    }
                )
                .addTo(this.leafletMap)
        })
    }

    private compilePopup(component, onAttach): any {
        const compFactory: any = this.resolver.resolveComponentFactory(component);
        let compRef: any = compFactory.create(this.injector);

        if (onAttach)
            onAttach(compRef);

        this.appRef.attachView(compRef.hostView);
        compRef.onDestroy(() => this.appRef.detachView(compRef.hostView));

        let div = document.createElement('div');
        div.appendChild(compRef.location.nativeElement);
        return div;
    }
}
