import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CourseDto, CourseService } from '@proxy/courses';
import { LessonDto } from '@proxy/lessons';

@Component({
  selector: 'app-lessons-list',
  templateUrl: './lessons-list.component.html',
  styleUrl: './lessons-list.component.scss',
  animations:[trigger('fade',[ 
    state('void',style({opacity:0})), 
    transition(':enter',[ animate(500) ]) 
  ]) 
]
})
export class LessonsListComponent implements OnInit {
  isPlaying: boolean = false;
  lessons : LessonDto[] = [];
  mainInfo = {} as CourseDto;
  /**
   *
   */
  constructor(private activatedRoute : ActivatedRoute,private service : CourseService,
    private router : Router
  ) { 
  }

  ngOnInit() {
    this.activatedRoute.paramMap.subscribe((response:any) => {
      var id = response.params.id;
      this.service.getCourseByIdById(id).subscribe((data:any) => { 
        this.mainInfo = data;      
        this.lessons = data.lessons.sort((a, b) => a.videoOrder - b.videoOrder);
      });
    })
  }

  playVideo() {
    this.isPlaying = true;
  }

  stopVideo() {
    this.isPlaying = false;
  }

  showVideo(url){
    // Create a link element
    const link = document.createElement('a');
    link.setAttribute('target', '_blank');
    link.href = url;
    link.download = "lesson"; // Use the file name
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }
}
