import { Component, OnInit } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { Post } from '../../model/post.type';

@Component({
  selector: 'app-list-posts',
  templateUrl: './list-posts.component.html'
})
export class ListPostsComponent implements OnInit {
  posts: Post[] = [];
  loading = false;
  error: string | null = null;

  constructor(private postsService: PostsService) {}

  ngOnInit(): void {
    this.loading = true;
    this.postsService.getPosts().subscribe({
      next: (data) => {
        console.log('Posts from API:', data); // checkpoint (console shows data)
        this.posts = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Could not load posts.';
        this.loading = false;
        console.error(err);
      }
    });
  }
}
