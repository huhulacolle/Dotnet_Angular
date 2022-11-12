import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import jwt_decode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  constructor(
    private router: Router
  ) { }

  clean(): void {
    localStorage.clear();
  }

  saveUser(token: string): void {
    localStorage.removeItem("Token");
    localStorage.setItem("Token", token);
  }

  removeUser(): void {
    localStorage.removeItem("Token");
    this.router.navigateByUrl("/")
  }

  isAdmin(): boolean {
    const token = localStorage.getItem("Token");
    const tokenDecoded: any = jwt_decode(token as string);
    if (tokenDecoded.role == "Admin") {
      return true;
    }
    return false

  }

  getUser(): any {
    const user = localStorage.getItem("Token");
    if (user) {
      return JSON.parse(user);
    }

    return {};
  }

  isLoggedIn(): boolean {
    const user = localStorage.getItem("Token");
    const helper = new JwtHelperService();

    if (user || !helper.isTokenExpired(user?.toString())) {
      return true;
    }
    return false;
  }

}
