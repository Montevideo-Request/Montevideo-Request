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
        this.tokenAssigned = localStorage.getItem("access_token") ? localStorage.getItem("access_token") : null;

        this.auth.getLoginEmitter().subscribe((token) => {
            this.tokenAssigned = token;
        });

        this.auth.getLogOutEmitter().subscribe(() => {
            this.tokenAssigned = null;
        });
    }
}

