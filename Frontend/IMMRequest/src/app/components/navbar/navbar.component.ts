import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { StorageService } from '../../services/storage.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  tokenAssigned: string;

  constructor(private auth: AuthenticationService, private storageService: StorageService) { }

  ngOnInit(): void {
    this.storageService.itemValue.subscribe((nextValue) => {
      this.tokenAssigned = nextValue;
    });
  }
}
