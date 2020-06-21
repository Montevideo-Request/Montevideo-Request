import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  token: string;

  constructor(private auth: AuthenticationService) { }

  ngOnInit(): void {
    //supongo que deberia controlar cambios al localstorage aca, para saber si mostrar el boton de login o el de logout
  }
}
