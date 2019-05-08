import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'priority'
})
export class PriorityPipe implements PipeTransform {

  transform(value: string, toNumber: boolean = false): any {
    let result: any 

    if(!toNumber){

      switch(value){
        case 'low':
          result = 'Niski'
        break
        case 'normal':
          result = 'Normalny'
        break
        case 'high':
          result = 'Wysoki'
        break
        case 'highest':
          result = 'Najwyższy'
        break
      }

    }
    else {
      switch(value){
        case 'low':
          result = 1
        break
        case 'normal':
          result = 2
        break
        case 'high':
          result = 3
        break
        case 'highest':
          result = 4
        break
      }
    }

    return result
  }

}
