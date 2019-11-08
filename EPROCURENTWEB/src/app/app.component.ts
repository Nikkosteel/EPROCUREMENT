import { Component, VERSION } from '@angular/core';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html'
    //,
    //styleUrls: ['./app.component.css']
})
export class AppComponent {
    title = 'Bienvenido al Tutorial de MVC 5 con Angular!';
    version = `${VERSION.major}.${VERSION.minor}`;
}