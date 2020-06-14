import { TestBed } from '@angular/core/testing';

import { AdditionalFieldService } from './additional-field.service';

describe('AdditionalFieldService', () => {
  let service: AdditionalFieldService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AdditionalFieldService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
