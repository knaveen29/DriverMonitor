import { React } from "react";

/**
 * Table row data for driver activity
 * @param {*} props row data
 * @returns 
 */
const TableRow = (props) => {
  var item = props.props;
  return (
    <tr key={item.id}>
      <td>{item.id}</td>
      <td>{item.driverId}</td>
      <td>{item.activityType}</td>
      <td>{item.createTime}</td>
      <td>{item.startTime}</td>
      <td>{item.endTime}</td>
    </tr>
  );
};

export default TableRow;
