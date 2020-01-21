import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './Nav.component.html',
  styleUrls: ['./Nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {} ;

  constructor(private authService: AuthService ) { }

  ngOnInit() {
  }

  login() {
    console.log(this.model) ;
    this.authService.login(this.model).subscribe(data => {
      console.log('logged successfully');
    }, error => {
      console.log('failed to login');
    });
  }
  logout() {
    localStorage.removeItem('token');
    console.log('logger out');
  }
  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }

}
