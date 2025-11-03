import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostsService } from '../../services/posts.service';
import { Post } from '../../model/post.type';

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html'
})
export class PostDetailComponent implements OnInit {
  post: Post | null = null;
  loading = false;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private postsService: PostsService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (!id) {
      this.error = 'Invalid post id';
      return;
    }

    this.loading = true;
    this.postsService.getPost(id).subscribe({
      next: (data) => {
        console.log('Single post:', data); // checkpoint
        this.post = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Could not load post.';
        this.loading = false;
        console.error(err);
      }
    });
  }
}
