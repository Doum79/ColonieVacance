export class User {
	public id: number;
	public email: string;
	public password: string;
	public profil: string;
	public gender: boolean;
	public firstName: string;
	public lastName: string;
	public street: string;
	public city: string;
	public zipCode: number;
	public country: string;
	public phoneNumber: number;
	public confirmPassword: string;

	public static parentProfil = "PARENT";

	constructor(id?: number, email?: string, password?: string, profil?: string, gender?: boolean, firstName?: string, lastName?: string,
		street?: string, city?: string, zipCode?: number, phoneNumber?: number, confirmPassword?: string)

	constructor(id: number, email: string, password: string, profil: string, gender: boolean, firstName: string, lastName: string,
		street: string, city: string, zipCode: number, phoneNumber: number, confirmPassword: string) {
		this.id = id;
		this.email = email;
		this.password = password;
		this.profil = profil;
		this.gender = gender;
		this.firstName = firstName;
		this.lastName = lastName;
		this.street = street;
		this.city = city;
		this.zipCode = zipCode;
		this.phoneNumber = phoneNumber;
		this.confirmPassword = confirmPassword;
	}
}
