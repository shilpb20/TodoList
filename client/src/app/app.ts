import { Component, signal } from '@angular/core';
import { Todo } from './features/todo/todo';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [Todo],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  protected readonly title = signal('ng-temp');
}
