export class Structure {
	public id: number;
	public email: string;
	public password: string;
	public profil: string;
	public street: string;
	public city: string;
	public postCode: string;
	public department: string;
	public state: string;
	public country: string;
	public phone: number;
	public longitude: string;
	public latitude: string;
	public siret: string;
	public name: string;
	public confirmPassword: string;

	public static structureProfil = "STRUCTURE";

	constructor(id?: number, email?: string, password?: string, profil?: string, street?: string, city?: string,
		postCode?: string, department?: string, state?: string, country?: string,  phone?: number, longitude?: string,
		latitude?: string, siret?: string, name?: string, confirmPassword?: string)

	constructor(id: number, email: string, password: string, profil: string, street: string, city: string,
		postCode: string, department: string, state: string, country: string, phone: number,
		longitude: string, latitude: string, siret: string, name: string, confirmPassword: string) {
		this.id = id;
		this.email = email;
		this.password = password;
		this.profil = profil;
		this.street = street;
		this.city = city;
		this.postCode = postCode;
		this.department = department;
		this.state = state;
		this.country = country;
		this.phone = phone;
		this.longitude = longitude;
		this.latitude = latitude;
		this.siret = siret;
		this.name = name;
		this.confirmPassword = confirmPassword;
	}
}
