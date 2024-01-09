import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { faBell } from '@fortawesome/free-solid-svg-icons'
import { AccountService } from './_services/account.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
model: any;
login() {
throw new Error('Method not implemented.');
}
  faBell = faBell
  title: string = 'Tinner !';

  constructor(private accountService: AccountService) { }
  setCurrentUser() {
    const userString = localStorage.getItem('user')
    if (!userString) return
    const user: User = JSON.parse(userString)
    this.accountService.setCurrentUser(user)
  } 
  ngOnInit(): void {
    this.setCurrentUser()
  }

  
  
}