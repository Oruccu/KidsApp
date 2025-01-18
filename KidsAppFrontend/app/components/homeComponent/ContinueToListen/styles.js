import { StyleSheet } from 'react-native';
import color from '@/app/styles/color';

const styles = StyleSheet.create({
  container: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    borderRadius: 40,
    padding: 10,
    margin: 10,
    marginBottom:20,
    marginTop:0,
    paddingVertical:15,
  },
  image: {
    width: 40,
    height: 40,
    marginLeft: 10,
  },
  text: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#FFFFFF', 
  },
  colors: color, // Redux renkleri için
});

export default styles;
