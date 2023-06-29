import { Pipe, PipeTransform } from '@angular/core';
import { IImage } from '../models/image.model';
import { IProjectPhoto } from '../models/project-photo.model';
import { environment } from 'src/environments/environment';
@Pipe({ name: 'photoToImage' })
export class ImagePipe implements PipeTransform {
  transform(input: IProjectPhoto[]): IImage[] {
    return input.map((f) => {
      return {
        path: f.link.includes("http") ? f.link : `${environment.filesBaseUrl}/${f.link}`
      };
    });
  }
}
