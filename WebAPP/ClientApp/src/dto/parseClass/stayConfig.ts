import { Stay } from "../stay";
import { Activity } from "../activity";
import { Thematic } from "../thematic";
import { Tag } from "../tag";

export class StayConfig {
	public stay: Stay;
	public activities: Array<Activity>;
	public thematics: Array<Thematic>;

	constructor(stay?: Stay, activities?: Array<Activity>, thematics?: Array<Thematic>)

	constructor(stay: Stay, activities: Array<Activity>, thematics: Array<Thematic>) {

		this.stay = stay;
		this.activities = activities;
		this.thematics = thematics;
	}
}
