import "./Pricing.css";

const Pricing = () => {
  return (
    <div className="pricing-container">
      <h1>Pricing</h1>
      <table>
        <tbody>
          <tr>
            <td>Standard Ticket</td>
            <td>$10</td>
          </tr>
          <tr>
            <td>Premium Ticket</td>
            <td>$14</td>
          </tr>
          <tr>
            <td>Family Ticket</td>
            <td>$35</td>
          </tr>
          <tr>
            <td>Standard Kids Ticket</td>
            <td>$8</td>
          </tr>
          <tr>
            <td>Premium Kids Ticket</td>
            <td>$10</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};
export default Pricing;
