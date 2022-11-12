import { AuthService } from './../../services/auth.service';
import { User } from './../../client/clientSwagger';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-login-register',
  templateUrl: './login-register.component.html',
  styleUrls: ['./login-register.component.css']
})
export class LoginRegisterComponent implements OnInit {

  email!: string;
  password!: string;
  isAdmin = false;

  constructor(
    private authService: AuthService,
    private storageService: StorageService,
    private router: Router
  ) { }

  ngOnInit(): void {
    if (this.storageService.isLoggedIn()) {
      this.router.navigateByUrl("/Post")
    }
  }

  register(): void {
    const user = new User();
    user.email = this.email;
    user.password = this.password;

    this.authService.register(user)
    .then(
      () => {
        this.login();
      }
    )
    .catch(
      error => {
        console.error(error);
      }
    )
  }

  login(): void {

    const user = new User();
    user.email = this.email;
    user.password = this.password;

    if (this.isAdmin) {
      // this.authService.loginAdmin(user)
      // .then(
      //   data => {
      //     this.storageService.saveUser(data.token);
      //     this.router.navigateByUrl("/Post")
      //   }
      // )
      // .catch(
      //   error => {
      //     console.error(error);
      //   }
      // )
      console.log("ça marche pas encore");

    }
    else {
      this.authService.login(user)
      .then(
        data => {
          this.storageService.saveUser(data.key);
          this.router.navigateByUrl("/Post")
        }
      )
      .catch(
        error => {
          console.error(error);
        }
      )
    }
  }

}
