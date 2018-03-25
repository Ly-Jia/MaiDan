import { TestBed, inject } from '@angular/core/testing';

import { BillbookService } from './billbook.service';

describe('BillbookService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BillbookService]
    });
  });

  it('should be created', inject([BillbookService], (service: BillbookService) => {
    expect(service).toBeTruthy();
  }));
});
