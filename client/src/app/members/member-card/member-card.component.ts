import { Component, Input } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { faUser ,faEnvelope,faHeart} from '@fortawesome/free-regular-svg-icons'
@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent {
  faUser = faUser
  faEnvelope = faEnvelope
  faHeart = faHeart
  @Input() member: Member | undefined
}
