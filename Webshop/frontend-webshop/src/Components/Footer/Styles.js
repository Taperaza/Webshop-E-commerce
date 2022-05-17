import styled from 'styled-components';
   
export const Box = styled.div`
  padding: 0px 0px;
  background: grey;
  position: "absolute";
  left: "0";
  bottom: "0";
  height: "60px";
  width: "100%";
  margin-top: auto;
  
   
  @media (max-width: 1000px) {
    padding: 20px 20px;
  }
`;
   
export const Container = styled.div`
    display: flex;
    flex-direction: column;
    justify-content: center;
    max-width: 1000px;
    margin: 0 auto;
    
  
    /* background: red; */
`
   
export const Column = styled.div`
  display: flex;
  flex-direction: column;
  text-align: left;
  margin-left: 50px;
`;
   
export const Row = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fill, 
                         minmax(185px, 1fr));
  grid-gap: 100px;
   
  @media (max-width: 1000px) {
    grid-template-columns: repeat(auto-fill, 
                           minmax(200px, 1fr));
  }
`;
   
export const FooterLink = styled.a`
  color: #fff;
  margin-bottom: 20px;
  font-size: 20px;
  text-decoration: none;
   
  &:hover {
      color: blue;
      transition: 200ms ease-in;
  }
`;
   
export const Heading = styled.p`
  font-size: 30px;
  color: #fff;
  margin-bottom: 30px;
  font-weight: bold;
`;
