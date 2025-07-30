import { bootstrapApplication } from '@angular/platform-browser';
import { App } from './app/app';
import { provideHttpClient } from '@angular/common/http';

bootstrapApplication(App, {
  providers: [
    provideHttpClient(), // <-- add it here once globally
    // other global providers if any
  ],
}).catch(err => console.error(err));
