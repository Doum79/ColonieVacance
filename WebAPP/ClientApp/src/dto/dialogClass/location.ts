export class DLocation {
	public longitude: number;
	public latitude: number;
	public department: string;
	public region: string

	constructor(longitude?: number, latitude?: number, department?: string, region?: string)

	constructor(longitude: number, latitude: number, departement: string, region: string) {
		this.longitude = longitude;
		this.latitude = latitude;
		this.department = departement;
		this.region = region;
	}
}