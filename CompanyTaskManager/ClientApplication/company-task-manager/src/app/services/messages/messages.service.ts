import { Injectable } from '@angular/core';
import { Message } from '../../models/Message'
@Injectable({
  providedIn: 'root'
})
export class MessagesService {
  constructor() { }

  public messages: Array<Message> = []
  private idCounter: number = 0

  public pushMessage(content: string, type: string, time: number){
    this.messages.push({content: content, type: type, id: this.idCounter})
    setTimeout(this.deleteMessage(this.idCounter), time * 1000)
    this.idCounter += 1
  }

  public deleteMessage(id: number): any{
    return () => {
      const messageIndex = this.messages.findIndex(m => m.id === id)
      this.messages.splice(messageIndex, 1)
    }
  }

}
