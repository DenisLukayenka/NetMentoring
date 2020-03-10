import { Component, Input, TemplateRef } from "@angular/core";

@Component({
    selector: 'pac-task-view',
    templateUrl: './task-view.component.html',
    styleUrls: ['./task-view.component.scss']
})
export class TaskViewComponent {
    @Input() header: TemplateRef<{ item: any }>;
    @Input() main: TemplateRef<{ item: any }>;
    @Input() footer: TemplateRef<{ item: any }>;
}