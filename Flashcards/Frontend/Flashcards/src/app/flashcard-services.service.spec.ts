import { TestBed } from '@angular/core/testing';

import { FlashcardServicesService } from './flashcard-services.service';

describe('FlashcardServicesService', () => {
  let service: FlashcardServicesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FlashcardServicesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
