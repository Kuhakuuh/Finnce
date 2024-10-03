import { TestBed } from '@angular/core/testing';

import { TinkService } from './tink.service';

describe('TinkService', () => {
  let service: TinkService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TinkService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
