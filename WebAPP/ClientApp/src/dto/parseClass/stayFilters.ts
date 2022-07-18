import { Activity } from "../activity";
import { StructurePlay } from "../structdisplay";
import { Thematic } from "../thematic";

export class StayFilters {
	public wordText: string;
	public startDate: Date;
	public stayCity: string;
	public thematics: Array<Thematic>;
	public yearList: Array<string>;
	public durationList: Array<number>;
	public activities: Array<Activity>;
	public structs: Array<StructurePlay>;
	public typesOfMois: Array<number>;
	public typesOfvanc: Array<number>;

	constructor(wordText?: string, startDate?: Date, stayCity?: string, typesOfMois?: Array<number>,typesOfvanc?: Array<number>,
		thematics?: Array<Thematic>, yearList?: Array<string>, durationList?: Array<number>, activities?: Array<Activity>, structs?: Array<StructurePlay>)

	constructor(wordText: string, startDate: Date, stayCity: string,typesOfMois: Array<number>, typesOfvanc: Array<number>,
		thematics: Array<Thematic>, yearList: Array<string>, durationList: Array<number>, activities: Array<Activity>, structs: Array<StructurePlay>) {

		this.wordText = wordText;
		this.startDate = startDate;
		this.stayCity = stayCity;
		this.thematics = thematics;
		this.yearList = yearList;
		this.durationList = durationList;
		this.activities = activities;
		this.structs = structs;
		this.typesOfMois = typesOfMois;
		this.typesOfvanc = typesOfvanc;
	}
}