import { Injectable } from "@angular/core";
import { HttpRequestMethods } from "../shared/functions/http-request-methods.class";
import { Structure } from "../dto/structure";
import { CreationObject } from "../shared/functions/creation-object";
import { AnonymousService } from "./anonymous-service";
import { StructurePlay } from "../dto/structdisplay";

@Injectable()
export class StructureService extends HttpRequestMethods {

    async update(uStructure: Structure) {
        var str = CreationObject.CreateStructure(uStructure);

        await this.getLatLong(str.street, parseInt(str.postCode)).then(rslt => {
            str.longitude = rslt["features"][0]["geometry"]["coordinates"][0];
            str.latitude = rslt["features"][0]["geometry"]["coordinates"][1];
        })

        return this.patch("structure", str);
    }

    async listStructure() {
        var rslt = await this.get("structure")
        var rsltTab = rslt as string[];

        var structureList = new Array<StructurePlay>();
        rsltTab.forEach(struct => {
            structureList.push(CreationObject.CreateStructure(struct));
        })
        return structureList;
    }

    addToFavorite(structureId: number) {
        return this.post("structure/favorite/" + structureId, null);
    }

    removeToFavorite(structureId: number) {
        return this.delete("structure/favorite/" + structureId, null);
    }

    favoriteStructuresList() {
        return this.get("structure/favorite");
    }
}