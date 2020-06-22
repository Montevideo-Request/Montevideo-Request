import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  itemValue = new BehaviorSubject(this.theItem);

  get theItem() {
    return localStorage.getItem('access_token');
  }
}