import { Injectable } from "@angular/core";
import { HttpRequestMethods } from "../shared/functions/http-request-methods.class";
import { Stay } from "../dto/stay";
import { StayFilters } from "../dto/parseClass/stayFilters";
import { CreationObject } from "../shared/functions/creation-object";
import { Activity } from "../dto/activity";
import { StayConfig } from "../dto/parseClass/stayConfig";
import { Thematic } from "src/dto/thematic";
import { Tag } from "src/dto/tag";
import { isNullOrUndefined } from "util";
import { UtilsFunctions } from "src/shared/functions/utils-functions";

@Injectable()
export class StayService extends HttpRequestMethods {

    public staysList: Array<Stay>;

    async listStaysByStructure(structureId: number) {
        var rslt = await this.get("stay/structure/" + structureId);
        var rsltArray = rslt as string[];

        var staysList = new Array<Stay>();
        rsltArray.forEach(stay => {
            staysList.push(CreationObject.CreateStay(stay));
        })
        return staysList;
    }

    async listStays() {
        var rslt = await this.getWithoutToken("stay");
        var rsltArray = rslt as string[];

        var staysList = new Array<Stay>();
        rsltArray.forEach(stay => {
            staysList.push(CreationObject.CreateStay(stay));
        })
        return staysList;
    }

    update(stay: Stay, activities: Array<Activity>, thematics: Array<Thematic>) {
        var stayConfig = new StayConfig(
            stay,
            activities,
            thematics,
        );

        return this.patch("stay", stayConfig);
    }

    create(stay: Stay, formData: FormData) {
        stay.furtherInformationsList.forEach(i => {
            i.withTransport = UtilsFunctions.StringToBoolean(i.withTransport);
        })

        formData.append("newStay", JSON.stringify(stay));
        return this.postFile("stay", formData);
    }

    remove(stays: Array<Stay>) {
        return this.post("stay/removeList", stays);
    }

    addToFavorite(stayId: number) {
        return this.post("stay/favorite/" + stayId, null);
    }

    removeToFavorite(stayId: number) {
        return this.delete("stay/favorite/" + stayId, null);
    }

    favoriteStaysList() {
        return this.get("stay/favorite");
    }

    async stayFilters(filters: StayFilters) {
        if(isNullOrUndefined(filters.startDate) || filters.startDate.toString() == "")
            filters.startDate == null;
            
        var rslt = await this.post("stay/filters", filters);
        var rsltTab = rslt as string[];

        var stayList = new Array<Stay>();
        rsltTab.forEach(stay => {
            stayList.push(CreationObject.CreateStay(stay));
        })

        return stayList;
    }

    async popularStays(){
        var rslt = await this.get("stay/populars");
        var rsltTab = rslt as string[];
        
        var stayList = new Array<Stay>();
        rsltTab.forEach(stay => {
            stayList.push(CreationObject.CreateStay(stay));
        })
        return stayList;
    }

    async lastMinutesStays(){
        return new Array<Stay>();
        var rslt = await this.get("stay/lastMinutes");
        var rsltTab = rslt as string[];

        var stayList = new Array<Stay>();
        rsltTab.forEach(stay => {
            stayList.push(CreationObject.CreateStay(stay));
        })

        return stayList;
    }

    async getStay(stayId: number){
        return CreationObject.CreateStay(await this.get(`stay/${stayId}`));
    }
}