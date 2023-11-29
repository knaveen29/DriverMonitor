import Card from 'react-bootstrap/Card';
import {React} from 'react';
import { DriverDetailsCardProps } from '../models/DriverDetailsCardProps.ts';

const DriverDetailsCard = (props : DriverDetailsCardProps) => {
  return (
    <Card
     bg={'secondary'}
          key={'secondary'}
          text={'secondary'.toLowerCase() === 'light' ? 'dark' : 'white'}
          style={{ width: '18rem' }}
          className="mb-2">
      <Card.Body>
        <Card.Title>{props.cardType}</Card.Title>
        <Card.Text>
          Total Data {props.driverDetails?.length}
        </Card.Text>
      </Card.Body>
    </Card>
  );
}

export default DriverDetailsCard;