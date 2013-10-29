{
	'includes': ['../build/common.gypi'],
	'targets': [
		{
			'type': 'static_library',
			'dependencies': [
				'../src/neolib.gyp:neo',
			],
			#'include_dirs+': [
			#'.'
			#],
			'direct_dependent_settings':{
				'include_dirs+':[
					'.',
				]
			},
			'target_name': 'cs2c',
			'sources': [
				'cs_to_c.c',
			],
		}
	],
}
