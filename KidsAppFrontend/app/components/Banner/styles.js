import { StyleSheet } from 'react-native';

const styles = StyleSheet.create({
  bannerContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    padding: 10,
    margin: 20,
  },
  imageContainer: {
    padding: 10,
    borderRadius: 40,
    // backgroundColor dinamik olarak ayarlanacak
  },
  image: {
    width: 60,
    height: 60,
    marginRight: 10,
  },
  score: {
    fontSize: 16,
    fontWeight: 'bold',
    padding: 10,
    marginRight: 10,
    // color dinamik olarak ayarlanacak
  },
  innerContainer: {
    flex: 1,
    alignItems: 'flex-end',
    borderTopRightRadius: 20,
    borderBottomRightRadius: 20,
    // backgroundColor dinamik olarak ayarlanacak
  },
});

export default styles;