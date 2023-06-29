import { environment } from "src/environments/environment";

export function getImageLink (image: string){
  return !image ?  `../assets/dummy.png` : image.includes("http") ? image : `${environment.filesBaseUrl}/${image}`
}